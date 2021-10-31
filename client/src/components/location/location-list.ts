import {EventAggregator} from 'aurelia-event-aggregator';
import { TestPortalAPI } from '../../api/agent';
import {LocationViewed, LocationUpdated } from '../messages';
import {observable} from 'aurelia-framework';
import {autoinject} from 'aurelia-dependency-injection';
  
@autoinject
export class LocationList {
  locations;
  selectedId = 0;
  public locales: { key: string; label: string }[];
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, ea: EventAggregator) { 
    ea.subscribe(LocationViewed, msg => this.select(msg.location));
    ea.subscribe(LocationUpdated, msg => {
      let id = msg.location.id;
      let found = this.locations.find(x => x.id == id);
      Object.assign(found, msg.location);
    });

  }

  created() {
    this.api.getAllLocations().then(locations => {
      this.locations = locations;
    });
  }

  select(location) {
    this.selectedId = location.id;
    return true;
  }
}
  

  