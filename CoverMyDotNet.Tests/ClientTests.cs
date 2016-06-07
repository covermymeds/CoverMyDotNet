using System.Net.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Moq;
using System;
using RestSharp;
using System.Linq;
using MockHttpServer;

namespace CoverMyDotNet.Tests
{
	[TestFixture]
	public class ClientTests
	{
		private Client _client;
		private string _apiId;
		private string _apiSecret;
		private const int DEFAULT_MOCK_PORT = 3333;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_apiSecret = Guid.NewGuid().ToString();
			_client = new Client (_apiId, _apiSecret);
			_client.BaseUrl = new Uri(string.Format("http://localhost:{0}", 
				DEFAULT_MOCK_PORT));	
		}

		[Test]
		public void Should_Default_Host()
		{
			Assert.AreEqual ("https://api.covermymeds.com/", 
				new Client(_apiId, _apiSecret).BaseUrl.ToString ());
		}

		[Test]
		public void Should_Have_Set_ApiId()
		{
			Assert.AreEqual(_apiId, _client.ApiId);
		}

		[Test]
		public void Should_HaveSet_Api_Secret()
		{
			Assert.AreEqual(_apiSecret, _client.ApiSecret);
		}

		[Test]
		public void Should_Client_Create_Request_Parse_Data()
		{
			string response = File.ReadAllText("Fixtures/Requests.json");
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/", "POST", (req, rsp, prm) => response)
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var resp = _client.CreateRequest(new RequestAttributes());
				Assert.AreEqual(resp.Id, "NT4HJ4");
				Assert.AreEqual(resp.Patient.FirstName, "Justin");
				Assert.AreEqual(resp.FormId, "highmark_west_virginia_prescription_drug_medication_6827");
				Assert.AreEqual(resp.Prescription.DrugId, "093563");
			}	
		}
		[Test]
		public void Should_Client_Create_Request_Post_Correct_Data()
		{
			string requestContent = "";
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/", "POST", (req, rsp, prm) => 
				{
					//we cant assert in lambdas
					requestContent = req.Content();
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.CreateRequest(new RequestAttributes()
				{
					Urgent = true,
					FormId = "Sample Form",
					Patient = new PatientAttributes()
					{
						FirstName = "Tom"
					}
				});
				Assert.GreaterOrEqual(requestContent.IndexOf("\"urgent\": true"), 0);
				Assert.GreaterOrEqual(requestContent.IndexOf("\"form_id\": \"Sample Form\""), 0);
			}
		}

		[Test]
		public void Should_Client_Get_Request_Parse_Data()
		{
			string response = File.ReadAllText("Fixtures/Requests.json");
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler(string.Format("/requests/{0}", id), "GET", (req, rsp, prm) => response)
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var resp = _client.GetRequest(id, token);
				Assert.AreEqual(resp.Id, "NT4HJ4");
				Assert.AreEqual(resp.Patient.FirstName, "Justin");
				Assert.AreEqual(resp.FormId, "highmark_west_virginia_prescription_drug_medication_6827");
				Assert.AreEqual(resp.Prescription.DrugId, "093563");
			}
		}

		[Test]
		public void Should_Client_Put_Request_Post_Correct_Data()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			string requestContent = "";
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler(string.Format("/requests/{0}", id), "PUT", (req, rsp, prm) => 
				{
					requestContent = req.Content();
				})

			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.UpdateRequest(id, token, "a test memo");
				Assert.GreaterOrEqual(requestContent.IndexOf("a test memo"), 0);
			}
		}
		
		[Test]
		public void Should_Client_Get_Requests_Params_Be_Correct()
		{
			var queryparams = new Dictionary<string, string>();
			var tokens = new List<string>();
			for(int i = 0;i<5;i++)
			{
				tokens.Add(Guid.NewGuid().ToString());
			}
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests", "GET", (req, rsp, prm) =>
				{
					queryparams = prm;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.GetRequests(tokens.ToArray());
				foreach(var v in tokens)
					Assert.GreaterOrEqual(queryparams.Values.First().IndexOf(v), 0);
			}
		}

		[Test]
		public void Should_Client_Get_Requests_Return_Multiple()
		{
			var tokens = new List<string>();
			for(int i = 0;i<5;i++)
			{
				tokens.Add(Guid.NewGuid().ToString());
			}
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests", "GET", (req, rsp, prm) =>
				{
					string resp = "{\"requests\": [";
					for(int i = 0;i<5;i++)
						resp += File.ReadAllText("Fixtures/Requests.json") + (i == 4 ? "" : ",");

					resp += "]}";
					return resp;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm)=> rsp.Header("Content-Type", "application/json")))
			{
				var resp = _client.GetRequests(tokens.ToArray());
				Assert.AreEqual(resp.Requests.Count, 5);
			}
		}
		[Test]
		public void Should_Client_Delete_Post_Correct_Data()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			string requestContent = "";
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/" + id, "DELETE", (req, rsp, prm) => 
				{
					requestContent = req.Content();
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.DeleteResponse(id, token, new RemoteUserAttributes()
				{
					DisplayName = "Steve"
				});
				Assert.GreaterOrEqual(requestContent.IndexOf("Steve"), 0);
			}	
		}

		[Test]
		public void Should_Client_Search_Drug_Use_Correct_Data()
		{
			string drug_name = Guid.NewGuid().ToString();
			Dictionary<string, string> requestContent = new Dictionary<string, string>();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/drugs", "GET", (req, rsp, prm) => 
				{
					requestContent = prm;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.SearchDrugs(drug_name);
				Assert.That(requestContent.Any(p => p.Key == "q"));
				Assert.That(requestContent.Any(p => p.Value == drug_name));
			}	
		}

		[Test]
		public void Should_Client_Search_Drug_Parse_Data()
		{
			string drug_name = Guid.NewGuid().ToString();
			Dictionary<string, string> requestContent = new Dictionary<string, string>();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/drugs", "GET", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/Drugs.json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var drugs = _client.SearchDrugs(drug_name);
				Assert.AreEqual(drugs.Drugs.Count, 3);
				Assert.AreEqual(drugs.Drugs.First().FullName, "Boniva 150MG tablets");
				Assert.AreEqual(drugs.Drugs.First().Strength, "150");
				Assert.AreEqual(drugs.Drugs[1].Href, "https://staging.api.covermymeds.com/drugs/094563");
				Assert.AreEqual(drugs.Drugs[2].GPI, "30042048102030");
			}	
		}


		[Test]
		public void Should_Client_Get_Drug_Use_Correct_Data()
		{
			string drug_id = Guid.NewGuid().ToString();
			bool called = false;
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler(string.Format("/drugs/{0}", drug_id), "GET", (req, rsp, prm) => 
				{
					called = true;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.GetDrug(drug_id);
				Assert.That(called);
			}
		}

		[Test]
		public void Should_Client_Get_Form_Parse_Data()
		{
			string form_id = Guid.NewGuid().ToString();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler(string.Format("/forms/{0}", form_id), "GET", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/Form.json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm)=> rsp.Header("Content-Type", "application/json")))
			{
				var form = _client.GetForm(form_id);
				Assert.AreEqual(form.Id, 4);
				Assert.AreEqual(form.Name, "humana_tracleer");
				Assert.AreEqual(form.IsEPA, false);
				Assert.AreEqual(form.ContactFax, "(877) 486-2621");
			}
		}

		[Test]
		public void Should_Client_Get_Form_Use_Correct_URL()
		{
			string form_id = Guid.NewGuid().ToString();
			bool called = false;
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler(string.Format("/forms/{0}", form_id), "GET", (req, rsp, prm) => 
				{
					called = true;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				_client.GetForm(form_id);
				Assert.That(called);
			}
		}

		[Test]
		public void Should_Client_Search_Forms_Parse_Data()
		{
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/forms/", "GET", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/Forms.Json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var forms = _client.SearchForms("1234", "OH", "12345");
				Assert.AreEqual(forms.Forms.Count, 5);
				Assert.AreEqual(forms.Forms.First().Id, 15257);
				Assert.AreEqual(forms.Forms[2].Name, "express_scripts_medicare_part_d_quantity_limit_exceptions");
				Assert.AreEqual(forms.Forms[1].Directions, "Prior Authorization of Benefits (PAB) Form for Multi-Source Brand Medications");
				Assert.AreEqual(forms.Forms.Last().RequestFormId, "anthem_general_3876");
			}
		}

		[Test]
		public void Should_Client_Search_Forms_Use_Correct_Data()
		{
			var requestContent = new Dictionary<string, string>();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/forms", "GET", (req, rsp, prm) => 
				{
					requestContent = prm;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				_client.SearchForms("1234", "OH", "12345");
				Assert.AreEqual(requestContent["drug_id"], "1234");
				Assert.AreEqual(requestContent["state"], "OH");
				Assert.AreEqual(requestContent["q"], "12345");
			}
		}
		
		[Test]
		public void Should_Client_Search_Forms_Use_Correct_Data2()
		{
			var requestContent = new Dictionary<string, string>();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/forms", "GET", (req, rsp, prm) => 
				{
					requestContent = prm;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				_client.SearchForms("1234", "OH", "30", "binsample", "223142", "group-567");
				Assert.AreEqual(requestContent["drug_id"], "1234");
				Assert.AreEqual(requestContent["state"], "OH");
				Assert.AreEqual(requestContent["threshold"], "30");
				Assert.AreEqual(requestContent["bin"], "binsample");
				Assert.AreEqual(requestContent["pcn"], "223142");		
				Assert.AreEqual(requestContent["group_id"], "group-567");
			}
		}

		[Test]
		public void Should_Client_Post_Tokens()
		{
			var tokenIds = new List<string>();
			string requestContent = "";
			for(int i = 0;i<5;i++)
				tokenIds.Add(Guid.NewGuid().ToString());
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/tokens", "POST", (req, rsp, prm) => 
				{
					requestContent = req.Content();
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.PostTokens(tokenIds.ToArray());
				for(int i = 0;i<5;i++)
					Assert.GreaterOrEqual(requestContent.IndexOf(tokenIds[i]), 0);
			}
		}

		[Test]
		public void Should_Client_Parse_Token_Data()
		{
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/tokens", "POST", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/PostToken.json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var resp = _client.PostTokens(new string []{"blah", "test", "Test2"});
				Assert.AreEqual(resp.Tokens.Count, 1);
				Assert.AreEqual(resp.Tokens.First().Id, "nhe44fu4g22upqqgstea");
				Assert.AreEqual(resp.Tokens.First().RequestId, "NT4HJ4");

			}
		}

		[Test]
		public void Should_Client_Delete_Token()
		{
			string _tokenId = Guid.NewGuid().ToString();
			bool called = false;
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/tokens/" + _tokenId, "DELETE", (req, rsp, prm) => 
				{
					called = true;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.DeleteToken(_tokenId);
				Assert.That(called);
			}
		}

		[Test]
		public void Should_Client_Post_Indicator_Correctly()
		{
			string requestContent = "";
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/indicators", "POST", (req, rsp, prm) => 
				{
					requestContent = req.Content();
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.PostIndicator(new IndicatorAttributes()
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
				Assert.That(requestContent, Is.StringContaining("\"first_name\": \"Sally\""));
				Assert.That(requestContent, Is.StringContaining("\"email\": \"example@example.com\""));
				Assert.That(requestContent, Is.StringContaining("\"bin\": \"773836\""));
				Assert.That(requestContent, Is.StringContaining("\"npi\": \"1234567890\""));
			}
		}

		[Test]
		public void Should_Client_Post_Indicator_Parse_Data_Correctly()
		{
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/indicators/", "POST", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/Indicator.json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				var resp = _client.PostIndicator(new IndicatorAttributes());
				Assert.AreEqual(resp.Patient.Address.State, "OH");
				Assert.AreEqual(resp.Payer.Bin, "015581");
				Assert.AreEqual(resp.Prescription.PARequired, false);
				Assert.AreEqual(resp.Prescription.Predicted, false);
				Assert.AreEqual(resp.Prescription.Autostart, false);
			}
		}
	}
}