import {autoinject} from 'aurelia-dependency-injection';
import { observable } from "aurelia-framework";
import {EventAggregator} from 'aurelia-event-aggregator';
import {ValidationControllerFactory, ValidationController, ValidationRules } from "aurelia-validation";
import Swal from 'sweetalert2';
import { TestPortalAPI } from './../api/agent';
import { BookingCreated, LocationCreated } from './messages';
import { BootstrapFormRenderer } from '../helper/bootstrap-form-renderer';


interface Location {
  id: number;
  locationName: string;
  availableSpace: number;
  createdAt: Date;
}

interface LocationDTO {
  locationName: string;
  availableSpace: number;
  createdAt: Date;
  userId: number;
}

@autoinject
export class LocationDetail {
  routeConfig;
  location: Location;
  originalLocation: Location;
  controller: ValidationController;
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, private ea: EventAggregator, private controllerFactory: ValidationControllerFactory) { 
    this.controller = this.controllerFactory.createForCurrentScope();
    
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.controller.addObject(this);
    this.controller.reset({ object: this.location, propertyName: 'located' });

  }

  public bind() {
    this.controller.validate();
  }


  create(p1,p2,p3,p4) {
    var newLocation: LocationDTO = {
      locationName: p1,
      availableSpace:p2,
      createdAt:p3,
      userId:p4
    }
    this.controller.validate()
    .then((validate) => {
      if(validate.valid) {
        return this.api.createLocation(newLocation).then(location => {
          this.location = <Location>location;
          this.originalLocation = JSON.parse(JSON.stringify(this.location));
          this.ea.publish(new LocationCreated(this.location));
          window.location.reload();
        });
      } else {
        console.log('E no enter');
        
      }
    });
      
    
  }

  reset(){
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, reset it!'
    }).then((result) => {
      if (result.isConfirmed) {
        window.location.reload();
      }
    })
  }

  
  canSave() {
    this.controller.validate()
    .then((validate) => {
      if(validate.valid) {
        console.log(validate.valid)
        return true;
      } else {
        console.log(validate.valid)
        return false;
      }
    });
  }

  canReset(params) {
    if (params === undefined) {
      return !this.api.isRequesting;
    }
  }


}

ValidationRules
  .ensure('locationName').required()
  .withMessage('Location Name must be provided.')
  .ensure('availableSpace').required()
  .withMessage('Available Space must be provided.')
  .ensure('userId').required()
  .withMessage('User ID must be provided.')
  .on(LocationDetail);
  
  

  