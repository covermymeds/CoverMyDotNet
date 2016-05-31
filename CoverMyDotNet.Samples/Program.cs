using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
namespace CoverMyDotNet.Samples
{
	class MainClass
	{
		public static List<ResponseAttributes> MultipleRequests(Client client)
		{
			var tokens = new List<string>();
			tokens.Add(client.CreateRequest(new RequestAttributes()
			{
				State = "OH",
				Prescription = new PrescriptionAttributes()
				{
					DrugId = "094563"
				},
				Patient = new PatientAttributes()
				{
					FirstName = "John",
					LastName = "test1",
					DateOfBirth = DateTime.Parse("3/4/1992"),
					PhoneNumber = "555-555-5555"
				},
				Payer = new PayerAttributes()
				{
					Bin = "773836",
					PCN = "MOCKPBM",
					GroupId = "ABC1",
				}
			}).Tokens.First().Id);
			tokens.Add(client.CreateRequest(new RequestAttributes()
			{
				State = "NY",
				Prescription = new PrescriptionAttributes()
				{
					DrugId = "094563"
				},
				Patient = new PatientAttributes()
				{
					FirstName = "Mark",
					LastName = "test3",
					DateOfBirth = DateTime.Parse("3/4/1990"),
					Email = "example@example.com"
				},
				Payer = new PayerAttributes()
				{
					Bin = "773836",
					PCN = "MOCKPBM",
					GroupId = "ABC1",
				}
			}).Tokens.First().Id);
			tokens.Add(client.CreateRequest(new RequestAttributes()
			{
				State = "OH",
				Prescription = new PrescriptionAttributes()
				{
					DrugId = "094563",
					Refills = 30
				},
				Patient = new PatientAttributes()
				{
					FirstName = "Tom",
					LastName = "test2",
					DateOfBirth = DateTime.Parse("4/4/1972")
				},
				Payer = new PayerAttributes()
				{
					Bin = "773836",
					PCN = "MOCKPBM",
					GroupId = "ABC1",
				}
			}).Tokens.First().Id);
			var requests = client.GetRequests(tokens.ToArray());
			foreach(var v in requests.Requests)
			{
				Console.WriteLine("Token: {0} {1}", v.Id, v.Tokens.First().Id);
			}
			return requests.Requests;
		}

		public static void DrugSearch(Client client)
		{
			var drugResults = client.SearchDrugs("ambien");
			Console.WriteLine(drugResults.Drugs.Count.ToString() + " Results");
			foreach(var v in drugResults.Drugs)
				Console.WriteLine(v.FullName + "  " + v.Id);
		}

		public static void GetDrug(Client client)
		{
			var drug = client.GetDrug("018058");
			Console.WriteLine(drug.FullName + " comes in the form of " + drug.DosageForm);
		}

		public static void GetForm(Client client)
		{
			var form = client.GetForm("medco_general_pa_form_21039");
			Console.WriteLine(form.Id + " " + form.Description);
		}

		public static void SearchForms(Client client)
		{
			var forms = client.SearchForms("064691", "OH", "bcbs");
			Console.WriteLine(forms.Forms.Count + " Results");
			Console.WriteLine("First form has a score of " + forms.Forms.First().Score);
		}

		public static void PostTokens(Client client, string[] tokens)
		{
			var result = client.PostTokens(tokens);
			foreach(var v in result.Tokens)
			{
				Console.WriteLine(v.Id + " : " + v.RequestId);
				Console.WriteLine(client.DeleteToken(v.Id).Content);
			}
		}

		public static void PostIndicator(Client client)
		{
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
			Console.WriteLine(indicator.Prescription.PARequired);
		}

		public static void SearchIndicators(Client client)
		{
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
			foreach(var v in indicator.Data.Prescriptions)
				Console.WriteLine("PA Required ? " + v.PARequired.ToString());
		}

		public static void Main (string[] args)
		{
			string apiId = ConfigurationManager.AppSettings ["ApiId"];
			string secret = ConfigurationManager.AppSettings ["Secret"];
			string host = ConfigurationManager.AppSettings ["Host"];			
			var client = new Client (apiId, secret);
			var requests = MultipleRequests(client);
			DrugSearch(client);
			GetDrug(client);
			GetForm(client);
			SearchForms(client);
			var tokens = new List<string>();
			foreach(var v in requests)
				tokens.Add(v.Tokens.First().RequestId);
			PostTokens(client, tokens.ToArray());
			PostIndicator(client);
			SearchIndicators(client);
		}
	}
}
