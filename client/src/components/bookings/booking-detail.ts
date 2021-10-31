import {autoinject} from 'aurelia-dependency-injection';
import { observable } from "aurelia-framework";
import {EventAggregator} from 'aurelia-event-aggregator';
import Swal from 'sweetalert2';
import { TestPortalAPI } from '../../api/agent';
import { BookingViewed, BookingUpdated } from '../messages';
import {areEqual} from '../../helper/utility';


interface Booking {
  email: string;
  status: string;
  testResult: string;
}


@autoinject
export class BookingDetail {
  routeConfig;
  booking: Booking;
  originalBooking: Booking;
  newBooking: Booking;
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, private ea: EventAggregator) { 

  }

  activate(params, routeConfig) {
    this.routeConfig = routeConfig;

    return this.api.getBooking(params.email).then(booking => {
      this.booking = <Booking>booking;
      this.routeConfig.navModel.setTitle(this.booking.email);
      this.originalBooking = JSON.parse(JSON.stringify(this.booking));
      this.ea.publish(new BookingViewed(this.booking));
    });
  }

  get canSave() {
    return this.booking.email && !this.api.isRequesting;
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
        if (!areEqual(params, this.originalBooking)) {
          return this.api.updateBooking(params).then(newBooking => {
            this.newBooking = <Booking>newBooking;
            this.ea.publish(new BookingUpdated(this.booking));
            Swal.fire(
              'Updated!',
              'Your record has been updated.',
              'success'
            )
            setTimeout(function(){location.replace("bookingList/")}, 1000);
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

  cancel(params) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, cancel it!'
    }).then((result) => {
      if (result.isConfirmed) {
        if (params !== undefined) {
          return this.api.cancelBooking(params).then(responseMessage => {
            Swal.fire(
              'Canceled!',
              'Your booking has been cancelled.',
              'success'
            )
            setTimeout(function(){location.replace("bookingList/")}, 1000);
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
    if (!areEqual(this.originalBooking, this.booking)) {
      let result = confirm('You have unsaved changes. Are you sure you wish to leave?');

      if (!result) {
        this.ea.publish(new BookingViewed(this.booking));
      }
      return result;
    }

    return true;
  }


}
