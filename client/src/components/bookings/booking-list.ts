import {EventAggregator} from 'aurelia-event-aggregator';
import { TestPortalAPI } from '../../api/agent';
import {BookingViewed, BookingUpdated } from '../messages';
import {observable} from 'aurelia-framework';
import {autoinject} from 'aurelia-dependency-injection';
  
@autoinject
export class BookingList {
  bookings;
  selectedId = 0;
  public locales: { key: string; label: string }[];
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, ea: EventAggregator) { 
    ea.subscribe(BookingViewed, msg => this.select(msg.booking));
    ea.subscribe(BookingUpdated, msg => {
      let id = msg.booking.email;
      let found = this.bookings.find(x => x.email == id);
      Object.assign(found, msg.booking);
    });

  }

  created() {
    this.api.getAllBookings().then(bookings => {
      this.bookings = bookings;
    });
  }

  select(booking) {
    this.selectedId = booking.email;
    return true;
  }
}
  

  