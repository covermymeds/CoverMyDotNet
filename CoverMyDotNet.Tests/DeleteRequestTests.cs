using System.Net.Http;
using System;
using NUnit.Framework;
using Moq;
using RestSharp;
using System.Linq;
using CoverMyDotNet.Requests;

namespace CoverMyDotNet.Tests
{
	[TestFixture]
	public class DeleteRequestTests
	{
		private string _apiId;
		private string _tokenId;
		private string _requestId;
		private DeleteRequestModel _deleteModel;
		private DeleteRequest _deleteRequest;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_tokenId = Guid.NewGuid().ToString();
			_requestId = Guid.NewGuid().ToString();
			_deleteModel = new DeleteRequestModel()
			{
				RemoteUser = new RemoteUserAttributes()
				{
					DisplayName = "Sally",
					FaxNumber = "555-555-5555",
					PhoneNumber = "444-444-4444"
				},
				TokenId = _tokenId
			};
			_deleteRequest = new DeleteRequest(_apiId, _tokenId, _requestId, _deleteModel);
		}

		[Test]
		public void Should_Use_Correct_Serializer()
		{
			Assert.IsInstanceOf<JsonSerializer>(_deleteRequest.JsonSerializer);
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_deleteRequest.Method, Method.DELETE);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_deleteRequest.Resource, string.Format("requests/{0}", _requestId));
		}

		[Test]
		public void Should_Be_JSON_Request_Format()
		{
			Assert.AreEqual(_deleteRequest.RequestFormat, DataFormat.Json);
		}
		
		[Test]
		public void Should_Set_Correct_Version()
		{
			var queryParams = _deleteRequest.Parameters.Where (p => p.Name == "v");
			Assert.That(queryParams.Any(p => p.Value == "1"));
		}

		[Test]
		public void Should_Set_Bearer()
		{
			var queryParams = _deleteRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Value.ToString() == string.Format("Bearer {0}+{1}", _apiId, _tokenId)));
		}

		[Test]
		public void Should_Set_Authorization_Parameter()
		{
			var queryParams = _deleteRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Name == "Authorization"));
		}

		[Test]
		public void Should_Have_Correct_RemoteAttributes()
		{
			var body = _deleteRequest.Parameters.Where(p => p.Type == ParameterType.RequestBody).First();
			Assert.GreaterOrEqual(body.Value.ToString().IndexOf("\"display_name\": \"Sally\""), 0);
			Assert.GreaterOrEqual(body.Value.ToString().IndexOf("\"fax_number\": \"555-555-5555\""), 0);
			Assert.GreaterOrEqual(body.Value.ToString().IndexOf("\"phone_number\": \"444-444-4444\""), 0);
		}

		[Test]
		public void Should_Have_Correct_Body_Token()
		{
			var body = _deleteRequest.Parameters.Where(p => p.Type == ParameterType.RequestBody).First();
			Assert.GreaterOrEqual(body.Value.ToString().IndexOf(
				string.Format("\"token_id\": \"{0}\"", _tokenId)), 0);
		}
	}
}