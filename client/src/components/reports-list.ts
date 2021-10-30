import {EventAggregator} from 'aurelia-event-aggregator';
import * as toastr from 'toastr';
import { TestPortalAPI } from '../api/agent';
import {observable} from 'aurelia-framework';
import {autoinject} from 'aurelia-dependency-injection';
  
@autoinject
export class ReportList {
  reports;
  selectedId = 0;
  public locales: { key: string; label: string }[];
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, ea: EventAggregator) { 

    
  }


  created() {
    this.api.getData().then(reports => {
      this.reports = reports;
      console.log(reports);
      toastr.error('I do not think that word means what you think it means.', 'Inconceivable!')
    });
  }

  select(report) {
    this.selectedId = report.id;
    return true;
  }
}
  

  