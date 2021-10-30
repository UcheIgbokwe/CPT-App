import {autoinject} from 'aurelia-dependency-injection';
import { observable } from "aurelia-framework";
import {EventAggregator} from 'aurelia-event-aggregator';
import {ValidationControllerFactory, ValidationController, ValidationRules } from "aurelia-validation";
import Swal from 'sweetalert2';
import { TestPortalAPI } from './../api/agent';
import { BootstrapFormRenderer } from '../helper/bootstrap-form-renderer';

interface User {
  fullName: string;
  gender: string;
  email: string;
  role: string;
}

interface UserDto {
  firstName: string;
  lastName: string;
  gender: string;
  email: string;
  role: string;
}

@autoinject
export class UserDetail {
  routeConfig;
  userInfo: User;
  originalUser: User;
  userList;
  controller: ValidationController;
  @observable
  public currentLocale: string;

  constructor(private api: TestPortalAPI, private ea: EventAggregator, private controllerFactory: ValidationControllerFactory) { 
    this.controller = this.controllerFactory.createForCurrentScope();
    
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.controller.addObject(this);
    this.controller.reset({ object: this.userInfo, propertyName: 'user' });
  

  }

  // public bind() {
  //   this.controller.validate();
  // }

  create(p1,p2,p3,p4,p5) {
    var newUser: UserDto = {
      firstName: p1,
      lastName:p2,
      gender:p3,
      email:p4,
      role:p5,
    }
    this.controller.validate()
    .then((validate) => {
      if(validate.valid) {
        return this.api.registerUser(newUser).then(userInfo => {
          this.userInfo = <User>userInfo;
          this.originalUser = JSON.parse(JSON.stringify(this.userInfo));
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

  bind() {
    this.api.getUsers().then(userList => {
      this.userList = userList;
    });
  }

}

ValidationRules
  .ensure('email').required().email()
  .withMessage('Valid Email must be provided.')
  .ensure('firstName').required()
  .withMessage('First Name must be provided.')
  .ensure('lastName').required()
  .withMessage('Last Name must be provided.')
  .ensure('role').required()
  .withMessage('Role must be provided.')
  .ensure('gender').required()
  .withMessage('Gender must be provided.')
  .on(UserDetail);
  
  

  