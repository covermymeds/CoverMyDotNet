# CoverMyMeds API

CoverMyDotNet is a dotnet library for easily interacting with the [CoverMyMeds API](https://developers.covermymeds.com/ehr-api.html/)

## Installation
#### Nuget
```
$ nuget install {NUGET_NAME}
```
#### Manual
```
Add the CoverMyDotNet.dll assembly to your project
```
## Building

## Getting Started
Once you have the library installed, it is very easy to use. First make sure you are using the CoverMyDotNet namespace. This can be done by adding this line to the top of your code file.
```
using CoverMyDotNet;
```
Now you will need to make a new client to interact with the api. The client needs to be supplied with both an apiId and an apiSecret. There are two ways of supplying these keys. By default, the client will use environment variables ```CMM_API_ID, CMM_API_SECRET and CMM_API_URL```, alternatively, a client can be instantiated with parameters.
```c#
Client client = new Client(); //makes a new client object with keys from the environment
```
or
```c#
Client client = new Client("apiId", "apiSecret", "https://www.alt.api.covermymeds.com"); //note that the third parameter is optional and if none is supplied, will default to https://www.api.covermymeds.com
```
### Create Request
Creates a new PA request and returns information relating to it.
```c#
var request = client.CreateRequest(new RequestAttributes()
{
  State = "OH",
  Patient = new PatientAttributes()
  {
    Name = "John"
  }
});
request.Patient.Name //John
request.Id //DF73GB
request.Tokens.First().Id //gq9vmqai2mkwewv1y55x Can be used to access the request in the future
```
### Get Request(s)
```c#
var request = client.GetRequest("DF73GB");
request.Patient.Name //John
var requests = client.GetRequests(new string[]{"DF73GB", "ABC123", "9HGtZo"}); 
requests.Requests //contains a list of requests
```
