using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
namespace CoverMyDotNet.Samples
{
	class MainClass
	{
		public static void MultipleRequests(Client client)
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
					DateOfBirth = "3/4/1992"
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
					DateOfBirth = "3/4/1990"
				}
			}).Tokens.First().Id);
			tokens.Add(client.CreateRequest(new RequestAttributes()
			{
				State = "OH",
				Prescription = new PrescriptionAttributes()
				{
					DrugId = "094563"
				},
				Patient = new PatientAttributes()
				{
					FirstName = "Tom",
					LastName = "test2",
					DateOfBirth = "4/4/1972"
				}
			}).Tokens.First().Id);
		}
		public static void Main (string[] args)
		{
			string apiId = ConfigurationManager.AppSettings ["ApiId"];
			string secret = ConfigurationManager.AppSettings ["Secret"];
			string host = ConfigurationManager.AppSettings ["Host"];			
			var client = new Client (apiId, secret);
			//MultipleRequests(client);
		}
	}
}
