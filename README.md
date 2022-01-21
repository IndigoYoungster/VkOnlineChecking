<h1 align="center">VkOnlineChecking</h1>

## About
This project is asp.net web api that allows you to collect statistics of online VK users.
All data is taken from the site databases vk.com using VkApi.
Api requests can be made using Swagger or any other suitable programs.
The output of the main query will result in an excel file that contains the user id and two fields "Datetime" and "Status".

## Run the program
In the file `appsettings.json` change `DefaultConnection` the connection string to the database to the one you need.

in the file `Services/QuartzJob/DataScheduler.cs` you can set the time for the background task of collecting statistics.\
`StartNow` - to start the task when the server starts.\
`StartAt(startTime)` - to start at a certain time. The time is specified in the `startTime` variable.\
`WithIntervalInMinutes()` - to set the interval between requests.

In the file `Services/StatisticsUpdate.cs` you will need to insert your service access key into the `_token` variable.
If you don't know where to find it, then read the first page of the VkApi documentation and you will understand everything.
Also check that the value of the `_version` variable corresponds to the current version of VkApi.

## Usage
`GET: api/Profiles` - get all user ids \
`GET: api/Profiles/{profileUri}` - get the id of a specific user \
`POST: api/Profiles/{profileUri}` - add a new user id to the database \
`DELETE: api/Profiles/{profileUri}` - delete the user id from the database

`GET: api/ProfilesStatistic` - get all user statistics \
`GET: api/ProfilesStatistic/{profileUri}` - download an excel spreadsheet with all statistics for a specific user \
`DELETE: api/ProfilesStatistic/{id}` - delete the statistics row by its id in the database
