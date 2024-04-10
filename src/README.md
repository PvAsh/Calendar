## Versions

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

This project includes a [DayPilot Lite for JavaScript](https://javascript.daypilot.org/open-source/).

This project uses axios to call web api

## License

This project is licensed under Apache License 2.0.

## Project Initialization

Run `npm install` to download all dependencies.

## Running the Project

Run `npm start` to start the built-in web server at [http://localhost:3000](http://localhost:3000).

## Build

Run `npm run build` to build the project.

**For API **
This is a Dot Net core 8 web api to ceter back end for calendar react application.

**Project Install**

Extract files in to a folder and open solution in visual studio preferable VS2022.

Restore nuget packages and run the application under IIS Express.
Make sure this application so react UI can get data from the end points.
**Application Informaion**


Application supports swagger API documentation.

It has two main controllers 

1. Users
2. Meeting

**Data Storage**


Data is stored in json file(users.json and Meeting.json) as this is just a test and can be run from anywhere so no need of sql, postgres or relational databases.
