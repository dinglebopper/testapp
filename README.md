# TestApp

This is a console application with a self-hosted web server. It's sole purpose
is to act as a fake SUT for potential applicants to demonstrate their ability
to use GIT, compile applications, and author/run tests.

# Screenshot

![Screenshot](https://github.com/dinglebopper/testapp/raw/master/TestApp/img/screenshot.png)

## Requirements

* Visual Studio 2019
* .NET 4.7.2

## Building

* Open the solution in Visual Studio and rebuild

## Running

* Run `TestApp\bin\Debug\TestApp.exe`

## Testing

* Run `nunit3-console WebTest\bin\Debug\WebTest.dll`
* Run `nunit3-console UnitTest\bin\Debug\UnitTest.dll`
