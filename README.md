# CPT-App

A Web Application built using Aurelia Framework, ASP.NET Core, EntityFrameworkCore.InMemory, Bootstrap and FluentValidation.

## Architecture

The Project is largely inspired by the Clean Architcture also known
as [Onion Architectue] and uses the [Command Query Responsibility Segregation (CQRS) Pattern] 

Follow the steps below to run the application:

# Prerequisities:
In order to use this application, you will need Aurelia CLI, .NET 5 and NodeJs.

If you don't have any, kindly install the following

1. Install NodeJS version 10 or above.

2. Install .NET 5 

3. Install a standard Git client eg GitBash and run "npm install -g aurelia-cli" on the Git client.

# API:
To startup the API project, follow these steps:

* Navigate to the [src.Infrastructure](src/Infrastructure) project folder
  `cd src/Infrastructure`
* Run the code below to create db migration. (Database has been pre-configured)
  `dotnet ef migrations add Testing`  
  `dotnet ef database update` 
* Navigate to the [src.API](src/API) project folder
  `cd ..`
  `cd API`
  `dotnet build`
* Startup the API project
  `dotnet run`

# CLIENT:
To startup the CLIENT project, follow these steps:

* Navigate to the [client](client) project folder
  `cd client`
* Run the code below to install dependencies.
  `npm install`  
* Startup the CLIENT project
  `npm start`

# NAVIGATING THE APPLICATION:
To begin booking on the application, Here are a few steps:

* The USER or ADMIN is required to register.
  `http://localhost:8080/`
* Details of every user is available on this page due to absence of authorization.
  `http://localhost:8080/`  
* The ADMIN can create a new location or update a location and available spaces.
  `http://localhost:8080/location/`  
* Details about a location are available on this page.
  `http://localhost:8080/location/`   
* USERS can book for a test.
  `http://localhost:8080/booking/`  
* Reports on the available resources and necessary data
  `http://localhost:8080/report/`    
  

Incase of doubt, kindly contact me via: uchehenryigbokwe@gmail.com.

