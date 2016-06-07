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
### Search Drugs
```c#
var drugResults = client.SearchDrugs("ambien");
drugResults // list of drug attributes
foreach(var v in drugResults.Drugs)
  Console.WriteLine(v.FullName + "  " + v.Id);
```

### Get Drugs
```c#
var drug = client.GetDrug("018058");
if(drug)
  Console.WriteLine(drug.FullName + " comes in the form of " + drug.DosageForm);
```

### Get Form
```c#
var form = client.GetForm("medco_general_pa_form_21039");
if(form)
  Console.WriteLine(form.Id + " " + form.Description);
```

### Search Forms
```c#
var forms = client.SearchForms("064691", "OH", "bcbs");
foreach(var v in forms)
  Console.WriteLine(v.Name);
```

### Post Indicators
```c#
var indicator = client.PostIndicator(new IndicatorAttributes()
		{
			Patient = new PatientAttributes()
			{
				FirstName = "Sally",
				LastName = "test4",
				DateOfBirth = DateTime.Parse("3/4/1990"),
				Email = "example@example.com",
				Address = new AddressAttributes()
			},
			Payer = new PayerAttributes()
			{
				Bin = "773836",
				PCN = "MOCKPBM",
				GroupId = "ABC1",
			},				
			Prescriber = new IndicatorPrescriberAttributes()
			{
				NPI = "1234567890",		
				LastName = "Dr. Jones",
				Address = new AddressAttributes()						
			},
			Pharmacy = new IndicatorPharmacyAttributes()
			{
				NPI = "1234567890",
				Address = new AddressAttributes()
			},
			Prescription = new IndicatorPrescriptionAttributes()
			{
				DrugId = "094563",
				Refills = 30,
				Quantity = 3					
			}
		});
		indicator.Prescription.PARequired //true
```
### Search Indicators
```c#
var indicator = client.SearchIndicators(new IndicatorSearchAttributes()
		{
			Patient = new PatientAttributes()
			{
				FirstName = "Bob",
				LastName = "test4",
				DateOfBirth = DateTime.Parse("3/4/1990"),
				Email = "example@example.com",
				Address = new AddressAttributes()
			},
			Payer = new PayerAttributes()
			{
				Bin = "773836",
				PCN = "MOCKPBM",
				GroupId = "ABC1",
			},				
			Prescriber = new IndicatorPrescriberAttributes()
			{
				NPI = "1234567890",		
				LastName = "Dr. Jones",
				Address = new AddressAttributes()						
			},
			Pharmacy = new IndicatorPharmacyAttributes()
			{
				NPI = "1234567890",
				Address = new AddressAttributes()
			},
			Prescriptions = new List<IndicatorPrescriptionAttributes>()
			{
				new IndicatorPrescriptionAttributes()
				{
					DrugId = "094563",
					Refills = 30,
					Quantity = 3	
				},
				new IndicatorPrescriptionAttributes()
				{
					DrugId = "018058",
					Refills = 30,
					Quantity = 13	
				}
			}
		});
		foreach(var v in indicator.Prescriptions)
				Console.WriteLine("PA Required ? " + v.PARequired.ToString());
```
### Request Pages
The CoverMyDotNet client will automatically follow 301 requests returned by the api.
```c#
var page = client.GetRequestPage("DF73GB","gq9vmqai2mkwewv1y55x");
foreach(var v in page.Forms)
{
  Console.WriteLine(v.Identifier + "\n");
  foreach(var q in v.QuestionSets)
    Console.WriteLine(q.Title);
}
```
