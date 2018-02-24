# Hair Salon

#### .NET MVC app that allows the user to make a list of stylists and their clients.

#### _By Chris Swanner_

## Description
_This is the Epicodus weekly project for week 3 of the C# course. Its purpose is to demonstrate understanding of SQL and databases._

#### _Hair Salon_
* Allows the user to add a new stylist
* Allows the user to add a new client and assign the client to an existing stylist
* Allows the user to see a list of all stylists
* Allows the user to select a stylist, see their details, and see a list of clients who belong to that stylist

### Specifications
* User can add a new stylist
  * sample input: stylist name "Chris Swanner"
  * sample output: new stylist with the name "Chris Swanner" is created
* User can add a new client to the list of clients and select and assign a stylist to the client
  * sample input: Create new client with name "Tyler Swanner" and stylist "Chris Swanner"
  * sample output: The client Tyler Swanner is added to the list of clients for Chris Swanner
* User can see a list of all stylists
  * sample input: click on the link labeled "Stylists"
  * sample output: a list of all stylists is displayed
* User can select a stylist, see their details, and see a list of clients who belong to that stylist
  * sample input: click on the stylist's name in the list of stylists
  * sample output: a list of all clients for that stylist is displayed


  ## Setup/Installation Requirements

  * _Clone this GitHub repository_

  ```
  git clone https://github.com/ChrisSwanner/HairSalon
  ```

  * _Install the .NET Framework and MAMP_

    .NET Core 1.1 SDK (Software Development Kit)

    .NET runtime.

    MAMP

  *    _Import the data into the database_

  Type the following into the mySql command line:
  ```
  CREATE DATABASE hair_salon;
  USE hair_salon;
  CREATE TABLE stylists ( id serial PRIMARY KEY, name VARCHAR(255), hire_date DATE, phone VARCHAR(255));
  CREATE TABLE clients ( id serial PRIMARY KEY, name VARCHAR(255), phone VARCHAR(255), notes VARCHAR(255), stylist_id INT, PRIMARY KEY (`id`));
  ```

  * _Start mySql server from mamp._

  * _Run the program_
    1. In the command line, cd into the project folder.
    2. In the command line, type dotnet restore. Enter.  It make take a few minutes to complete this process.
    3. In the command line, type dotnet build. Enter. Any errror messages will be displayed in red.  Errors will need to be corrected before the app can be run. After correcting errors and saving changes, type dotnet build again.  When message says Build succeeded in green, proceed to the next step.
    4. In the command line, type dotnet run. Enter.

  * _View program on web browser at port localhost:5000/_

  * _Follow the prompts._

## Technologies Used

* HTML
* Bootstrap
* C#
* MAMP
* .Net Core 1.1
* Razor
* MySQL

### License

*MIT License*

Copyright (c) 2018 **_Chris Swanner_**
