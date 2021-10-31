import { TestPortalAPI } from '../../api/agent';
import {observable} from 'aurelia-framework';
import {autoinject} from 'aurelia-dependency-injection';
  
@autoinject
export class ReportList {
  reports;
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI) { 
    
  }


  bind() {
    this.api.getData().then(reports => {
      this.reports = reports;
    });
  }

  nameLength(row) {
    return row.locationName.length;
}

}
  

  