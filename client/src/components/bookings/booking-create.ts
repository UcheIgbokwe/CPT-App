import {autoinject} from 'aurelia-dependency-injection';
import { observable } from "aurelia-framework";
import {EventAggregator} from 'aurelia-event-aggregator';
import {ValidationControllerFactory, ValidationController, ValidationRules } from "aurelia-validation";
import Swal from 'sweetalert2';
import { TestPortalAPI } from '../../api/agent';
import {BookingCreated} from '../messages';
import { BootstrapFormRenderer } from '../../helper/bootstrap-form-renderer';


interface Booking {
  email: string;
  locationId: number;
  status: string;
  testResult: string;
  testType: string;
  testDate: Date;
}

interface BookingDto {
  email: string;
  locationId: number;
  testType: string;
  testDate: Date;
}

@autoinject
export class BookingDetail {
  routeConfig;
  booking: Booking;
  originalBooking: Booking;
  controller: ValidationController;
  public locales: { key: string; label: string }[];
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, private ea: EventAggregator, private controllerFactory: ValidationControllerFactory) { 
    this.controller = this.controllerFactory.createForCurrentScope();
    
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.controller.addObject(this);
    this.controller.reset({ object: this.booking, propertyName: 'booked' });
  

  }

  public bind() {
    this.controller.validate();
  }

  create(p1,p2,p3,p4) {
    var newBooking: BookingDto = {
      email: p1,
      locationId:p2,
      testType:p3,
      testDate:p4,
    }
    this.controller.validate()
    .then((validate) => {
      if(validate.valid) {
        return this.api.createBooking(newBooking).then(booking => {
          this.booking = <Booking>booking;
          this.originalBooking = JSON.parse(JSON.stringify(this.booking));
          this.ea.publish(new BookingCreated(this.booking));
          setTimeout(function(){window.location.replace("bookingList/")}, 1000);
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
  .ensure('email').required().email()
  .withMessage('Valid Email must be provided.')
  .ensure('locationId').required()
  .withMessage('Location ID must be provided.')
  .ensure('testType').required()
  .withMessage('Test type must be provided.')
  .on(BookingDetail);
  
  

  