Football Stats v2
===================

- Console application with argument handling
- Asp.Net MVC with bootstrap and JQuery
- Test driven development
- Repository Pattern
- CSV Parsing
- Moq
- Dependency Injection with Unity
- Lazy loading
- Template Pattern
- OO design
- Html Scraping using Agility Pack

## About ##

This solution was put together for displaying how the above programming concepts are utilised even for a very minimalistic application that allows for extensibility, testability and maintanibility. To get something up and running, the example was extended on top of the [football stats project v1](https://github.com/dejanvasic85/FootballStats).

The console application will display a menu on available tasks/commands and to execute them begin by supplying "-exe" "CommandName" format. It will be clear what to do when opening.

The website feature is very simple and includes a html table layout of the current English Premier League standings and highlights the teams that play a significant part towards the end.


## Running the Console ##

Open the solution, set the Football project to start-up and run! You will get the help menu with some available commands. You can always see this by entering *help* or *?* or *h*.

To close the application enter *quit* or *q* or *exit*.

## Running the web application ##

Opent the solution, set the Football.Web project to start-up and run!

Currently, the web application is configured to use html scraper as a repository so no database is required.
