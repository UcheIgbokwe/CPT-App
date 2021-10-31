import {HttpClient, json} from 'aurelia-fetch-client';
import {inject} from 'aurelia-framework';
import * as toastr from 'toastr';

@inject(HttpClient)
export class TestPortalAPI{
  isRequesting = false;

  constructor(private http: HttpClient) {
    const baseUrl = 'http://localhost:5000/api/';

    http.configure(config => {
      config.withBaseUrl(baseUrl);
    })
  }
  getData(){
    this.isRequesting = true;
    return this.http.fetch('Report')
      .then(response => response.json())
      .then(reports => {
        this.isRequesting = false;
        return reports;
      })
    .catch(error => {
      this.isRequesting = false
      toastr.error(error, 'Error!')
      return [];
    });  
  }

  getUsers(){
    this.isRequesting = true;
    return this.http.fetch('User')
      .then(response => response.json())
      .then(userList => {
        this.isRequesting = false;
        return userList;
      })
    .catch(errorLog => {
      this.isRequesting = false
      toastr.error(errorLog, 'Error!')
      return [];
    });  
  }

  registerUser(user){
    this.isRequesting = true;
    return this.http.fetch('User/RegisterUser', {
      method: 'post',
      body: json(user)
    })
    .then(response => response.json())
    .then(createdUser => {
      this.isRequesting = false;
      return createdUser;
    })
    .catch(error => {
      this.isRequesting = false;
      toastr.error(error, 'Error!');
      return [];
    });  
  }

  createBooking(booking){
    this.isRequesting = true;
    return this.http.fetch('Booking/CreateBooking', {
      method: 'post',
      body: json(booking)
    })
    .then(response => response.json())
    .then(createdBooking => {
      this.isRequesting = false;
      return createdBooking;
    })
    .catch(errorBooking => {
      this.isRequesting = false;
      toastr.error(errorBooking, 'Error!');
      return [];
    });
  }

  createLocation(locationDetails){
    this.isRequesting = true;
    return this.http.fetch('Location/Create', {
      method: 'post',
      body: json(locationDetails)
    })
    .then(response => response.json())
    .then(createdLocation => {
      this.isRequesting = false;
      return createdLocation;
    })
    .catch(errorLocation => {
      this.isRequesting = false;
      toastr.error(errorLocation, 'Error!');
      return [];
    });
  }

  updateLocation(locationDetails){
    this.isRequesting = true;
    return this.http.fetch('Location/Update', {
      method: 'put',
      body: json(locationDetails)
    })
    .then(response => response.json())
    .then(createdBooking => {
      this.isRequesting = false;
      return createdBooking;
    })
    .catch(errorUpdateLocation => {
      this.isRequesting = false;
      toastr.error(errorUpdateLocation, 'Error!');
      return [];
    });
  }



}
