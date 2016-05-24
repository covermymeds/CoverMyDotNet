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
	public class GetRequestTests
	{
		private string _apiId;
		private GetRequest _getRequest;
		private string _tokenId;
		private string _requestId;

		[SetUp]
		public void Setup()
		{
			_tokenId = Guid.NewGuid ().ToString ();
			_requestId = Guid.NewGuid ().ToString ();
			_apiId = Guid.NewGuid ().ToString ();
			_getRequest = new GetRequest(_apiId, _tokenId, _requestId);
		}

		[Test]
		public void Should_Set_Method()
		{
			Assert.AreEqual (Method.GET, _getRequest.Method);
		}

		[Test]
		public void Should_Set_Resource()
		{
			Assert.AreEqual (string.Format("requests/{0}", _requestId), _getRequest.Resource);
		}
		[Test]
		public void Should_Set_Bearer()
		{
			var queryParams = _getRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Value.ToString() == string.Format("Bearer {0}+{1}", _apiId, _tokenId)));
		}

		[Test]
		public void Should_Set_Authorization_Parameter()
		{
			var queryParams = _getRequest.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Name == "Authorization"));
		}

		[Test]
		public void Should_Set_Correct_Version()
		{
			var queryParams = _getRequest.Parameters.Where (p => p.Name == "v");
			Assert.That(queryParams.Any(p => p.Value == "1"));
		}

		[Test]
		public void Should_Set_Root_Element()
		{
			Assert.AreEqual(_getRequest.RootElement, "request");
		}
		
		[Test]
		public void Should_Set_ContentType()
		{
			Assert.AreEqual (DataFormat.Json, _getRequest.RequestFormat);
		}

	}
}