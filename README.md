# Status

## 9/1/23
Cloned repo from https://github.com/TimZander/BGVTakeHome and created README.

## 9/5/23
- Updated README with dev notes; updated gitignore to ignore vscode files.
- Implemented Get Folio
- Implemented Add Folio Transaction

## 9/6/23
Project is feature complete after implementing the Email Service and adding a test.  Run via the steps below, making sure to add your Mailgun API key.

# Dev Notes

## Dev Environment Setup
- Install VS Code ( https://code.visualstudio.com/download )
- Install dotnet SDK 6.0 ( https://dotnet.microsoft.com/en-us/download )
- Install c# dev kit for VS Code ( https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit )
- Install RestSharp for dotnet
  > dotnet add package RestSharp --version 110.2.0
- Install SQLite viewer ( https://dotnet.microsoft.com/en-us/download )
- Sign up for Mailgun, get an API key, and register cnelson0641@gmail.com as an authorized recipient
- Reboot (because Windows)

## Build and Run
- Edit the gitignored file MAILGUN_API_KEY.txt with your api key
- Run the application, which runs a webserver at https://localhost:5001/
  > cmd.exe /c app_driver.cmd
- Run the tests, which currently test the email functionality
  > cmd.exe /c app_driver.cmd

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
