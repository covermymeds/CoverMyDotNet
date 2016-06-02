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
	public class PutRequestTests
	{
		private string _apiId;
		private string _requestId;
		private string _tokenId;
		private string _memo;

		private PutRequest _putRequest;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_requestId = Guid.NewGuid().ToString();
			_tokenId = Guid.NewGuid().ToString();
			_memo = "I am a test memo";

			_putRequest = new PutRequest (_apiId, _requestId, _tokenId, _memo);
		}

		[Test]
		public void Should_Use_Post_Method()
		{
			Assert.AreEqual(_putRequest.Method, Method.PUT);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_putRequest.Resource, string.Format("requests/{0}", _requestId));
		}

		[Test]
		public void Should_Use_Correct_Root_Element()
		{
			Assert.AreEqual(_putRequest.RootElement, "request");
		}

		[Test]
		public void Should_Set_Bearer()
		{
			var queryParams = _putRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Value.ToString() == string.Format("Bearer {0}+{1}", _apiId, _tokenId)));
		}

		[Test]
		public void Should_Set_Authorization_Parameter()
		{
			var queryParams = _putRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Name == "Authorization"));
		}

		[Test]
		public void Should_Set_Correct_Version()
		{
			var queryParams = _putRequest.Parameters.Where (p => p.Name == "v");
			Assert.That (queryParams.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}

		[Test]
		public void Should_Correctly_Serialize_Data()
		{
			var body = _putRequest.Parameters.Where(p => p.Type == ParameterType.RequestBody).First();
			Assert.GreaterOrEqual(body.ToString().IndexOf(string.Format("\"memo\": \"{0}\"", _memo)), 0);
		}
	}
}