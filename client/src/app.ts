import {Router, RouterConfiguration} from 'aurelia-router';
import {inject, PLATFORM} from 'aurelia-framework';

import { TestPortalAPI } from './api/agent';

@inject(TestPortalAPI) 
export class App {
  router: Router;

  constructor(public api: TestPortalAPI) {}

  configureRouter(config: RouterConfiguration, router: Router){
    config.title = 'Test Booking Portal';
    config.options.pushState = true;
    config.options.root = '/';
    config.map([
      { route: '',                moduleId: PLATFORM.moduleName('./components/booking-create'), title:'Bookings' }
    ]);

    this.router = router;
  }
}
