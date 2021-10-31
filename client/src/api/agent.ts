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

  //Report
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

  //Users
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

  //Bookings
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

  getBooking(email){
    this.isRequesting = true;
    return this.http.fetch(`Booking/${email}`)
      .then(response => response.json())
      .then(booking => {
        this.isRequesting = false;
        return booking;
      })
    .catch(error => {
      this.isRequesting = false;
      toastr.error(error, 'Error!')
      return [];
    });
  }

  getAllBookings(){
    this.isRequesting = true;
    return this.http.fetch('Booking')
      .then(response => response.json())
      .then(bookingList => {
        this.isRequesting = false;
        return bookingList;
      })
    .catch(errorLogAllLocation => {
      this.isRequesting = false
      toastr.error(errorLogAllLocation, 'Error!')
      return [];
    });  
  }

  updateBooking(updateDetails){
    console.log(updateDetails)
    this.isRequesting = true;
    return this.http.fetch('Booking/UpdateTest', {
      method: 'put',
      body: json(updateDetails)
    })
    .then(response => response.json())
    .then(updateBooking => {
      this.isRequesting = false;
      return updateBooking;
    })
    .catch(errorUpdateBooking => {
      this.isRequesting = false;
      toastr.error(errorUpdateBooking, 'Error!');
      return [];
    });
  }

  cancelBooking(email){
    this.isRequesting = true;
    return this.http.fetch('Booking/CancelBooking', {
      method: 'put',
      body: json(email)
    })
    .then(response => response.json())
    .then(cancelBooking => {
      this.isRequesting = false;
      return cancelBooking;
    })
    .catch(errorCancelBooking => {
      this.isRequesting = false;
      toastr.error(errorCancelBooking, 'Error!');
      return [];
    });
  }

  //Location
  getAllLocations(){
    this.isRequesting = true;
    return this.http.fetch('Location')
      .then(response => response.json())
      .then(locationList => {
        this.isRequesting = false;
        return locationList;
      })
    .catch(errorLogAllLocation => {
      this.isRequesting = false
      toastr.error(errorLogAllLocation, 'Error!')
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

  getLocation(id){
    this.isRequesting = true;
    return this.http.fetch(`Location/${id}`)
      .then(response => response.json())
      .then(location => {
        this.isRequesting = false;
        return location;
      })
    .catch(error => {
      this.isRequesting = false;
      toastr.error(error, 'Error!')
      return [];
    });
  }

  delete(id){
    this.isRequesting = true;
    return this.http.fetch(`Location/delete/${id}`, {
      method: 'delete'
    })
    .then(response => response.text())
    .then(responseMessage => {
      this.isRequesting = false;
      console.log(responseMessage)
      return responseMessage;
    })
    .catch(error => {
      this.isRequesting = false;
      console.log(error);
      toastr.error(error, 'Error!')
    });
  }  

}
