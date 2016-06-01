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
		public void Should_Client_Delete_Post_Correct_Data()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			string requestContent = "";
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/requests/", "DELETE", (req, rsp, prm) => 
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
		public void Should_Client_Request_Pages_Use_Correct_Resouce()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			bool called = false;
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/request-pages/" + id, "GET", (req, rsp, prm) => 
				{
					called = true;
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers))
			{
				_client.GetRequestPage(id, token);
				Assert.That(called);
			}	
		}

		[Test]
		public void Should_Client_Request_Pages_Parse_Data_Correctly()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/request-pages/" + id, "GET", (req, rsp, prm) => 
				{
					return File.ReadAllText("Fixtures/RequestPage.json");
				})
			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm)=> rsp.Header("Content-Type", "application/json")))
			{
				var page = _client.GetRequestPage(id, token);
				Assert.AreEqual(page.Forms.Count, 1);
				Assert.AreEqual(page.Forms.First().Identifier, "pa_request");
				Assert.AreEqual(page.Forms.First().QuestionSets.Count, 5);
				Assert.AreEqual(page.Forms.First().QuestionSets.First().Questions.Count, 1);
				Assert.AreEqual(page.Forms.First().QuestionSets.First().Questions.First().QuestionType, "STATEMENT");
				Assert.AreEqual(page.Actions.Count, 1);
				Assert.AreEqual(page.Actions.First().Title, "Change Outcome");
				Assert.That(page.ProvidedCodedReferences, Is.Empty);
				Assert.AreEqual(page.Validations.Count, 2);
				Assert.AreEqual(page.Validations.Last().Type, "REGEX");
				Assert.That(page.Validations.First().Message, Is.Null);
			}	
		}

		[Test]
		public void Should_Client_Request_Page_Follow_303()
		{
			string id = Guid.NewGuid().ToString();
			string token = Guid.NewGuid().ToString();
			bool called = false;
			var requestHandlers = new List<MockHttpHandler>()
			{
				new MockHttpHandler("/request-pages/" + id, "GET", (req, rsp, prm) => 
				{
					rsp.AddHeader("Location", string.Format("http://localhost:{0}/request-pages/{1}/choose-form?token_id=1234", 
																DEFAULT_MOCK_PORT, id));
					rsp.StatusCode(303);
				}),
				new MockHttpHandler("/request-pages/" + id + "/choose-form", "GET", (req, rsp, prm) => 
				{
					called = true;
				})

			};
			using(new MockServer(DEFAULT_MOCK_PORT, requestHandlers, (req, rsp, prm) => rsp.Header("Content-Type", "application/json")))
			{
				_client.GetRequestPage(id, token);
				Assert.That(called);
			}
		}
	}
}

