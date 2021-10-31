import {autoinject} from 'aurelia-dependency-injection';
import { observable } from "aurelia-framework";
import {EventAggregator} from 'aurelia-event-aggregator';
import Swal from 'sweetalert2';
import { TestPortalAPI } from '../../api/agent';
import { LocationViewed, LocationUpdated } from '../messages';
import {areEqual} from '../../helper/utility';


interface Location {
  id: number;
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
  newLocation: Location;
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, private ea: EventAggregator) { 

  }

  activate(params, routeConfig) {
    this.routeConfig = routeConfig;

    return this.api.getLocation(params.id).then(location => {
      this.location = <Location>location;
      this.routeConfig.navModel.setTitle(this.location.locationName);
      this.originalLocation = JSON.parse(JSON.stringify(this.location));
      this.ea.publish(new LocationViewed(this.location));
    });
  }

  get canSave() {
    return this.location.locationName && this.location.availableSpace && this.location.createdAt && this.location.id && this.location.userId && !this.api.isRequesting;
  }

  get canUpdate() {
    return 
  }

  update(params) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, update it!'
    }).then((result) => {
      if (result.isConfirmed) {
        if (!areEqual(params, this.originalLocation)) {
          return this.api.updateLocation(params).then(newLocation => {
            //check if this returns userId
            this.newLocation = <Location>newLocation;
            this.ea.publish(new LocationUpdated(this.newLocation));
            Swal.fire(
              'Updated!',
              'Your record has been updated.',
              'success'
            )
            setTimeout(function(){location.replace("/")}, 3000);
          });
        }else{
          Swal.fire(
            'Warning',
            'There are no new changes',
            'warning',
          );
        }
        
      }
    })
  }

  delete(params) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        if (params !== undefined) {
          return this.api.delete(params.id).then(responseMessage => {
            Swal.fire(
              'Deleted!',
              'Your record has been deleted.',
              'success'
            )
            setTimeout(function(){location.replace("/")}, 3000);
          });
          
          
        }else{
          Swal.fire(
            'Warning',
            'The fields are empty!',
            'warning',
          );
        }
        
      }
    })
  }

  canDeactivate() {
    if (!areEqual(this.originalLocation, this.location)) {
      let result = confirm('You have unsaved changes. Are you sure you wish to leave?');

      if (!result) {
        this.ea.publish(new LocationViewed(this.location));
      }
      return result;
    }

    return true;
  }


}
