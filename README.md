# Status

## 9/1/23
Cloned repo from https://github.com/TimZander/BGVTakeHome and created README.

## 9/5/23
- Updated README with dev notes; updated gitignore to ignore vscode files.
- Implemented Get Folio
- Implemented Add Folio Transaction

# Dev Notes

## Dev Environment Setup
- Install VS Code ( https://code.visualstudio.com/download )
- Install dotnet SDK 6.0 ( https://dotnet.microsoft.com/en-us/download )
- Install RestSharp for dotnet
  > dotnet add package RestSharp --version 110.2.0
- Install SQLite viewer ( https://dotnet.microsoft.com/en-us/download )
- Reboot (because Windows)

## Build and Run
- Open new PowerShell session and verify dotnet 6.0 is installed
  > dotnet --list-sdks
- Change directories to RoomCharges
  > cd RoomCharges
- Build the project
  > dotnet build
- Run the app
  > dotnet run

## View app and db
- View the app at https://localhost:5001/
- View the DB using 'DB Browser' program and open 'RoomCharges.db' in RoomCharges/Data/ dir

# Project Requirements from BGV

## Introduction
This is a take home project for Breckenridge Grand Vacations. Please do as much as you are able in roughly two hours. This a simple application, that allow users/business employees to track and add charges to guest's rooms during there stay. When you first start up the appication you will see a the list of business that you can select. After selecting one of the business, you should be able to search the guests, based on the parameter you see on the top of the page. After searching, you are able to select a guest's reservation and add a transaction to that guests reservation as well as list out all of their transaction history.

## Getting Started
Included is a Blazor Server application which can be run with dotnet core and VS Code or Visual Studio.
There is a SQLite database which can be read with free GUI applications(ex: https://sqlitebrowser.org/dl/) or by command line interface.

## Build and Test
dotnet build from the command line will build the solution. dotnet run will start a local instance for you to run the application.

## Contribute
There are three locations within the application which throw a NotImplimentedException. Please impliment the features as needed to complete the application.
First priority is getting the Folio Transactions, then adding a new Folio Transaction and finally sending an email via a 3rd party API. Mailgun has a free tier which does not require any payment information to use. You are free to use other APIs if desired.
