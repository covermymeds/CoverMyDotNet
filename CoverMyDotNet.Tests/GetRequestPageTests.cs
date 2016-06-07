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
	public class GetRequestPagesTests
	{
		private string _apiId;
		private GetRequestPage _getRequestPage;
		private string _tokenId;
		private string _requestId;

		[SetUp]
		public void Setup()
		{
			_tokenId = Guid.NewGuid ().ToString ();
			_requestId = Guid.NewGuid ().ToString ();
			_apiId = Guid.NewGuid ().ToString ();
			_getRequestPage = new GetRequestPage(_apiId, _requestId, _tokenId);
		}

		[Test]
		public void Should_Set_Method()
		{
			Assert.AreEqual (Method.GET, _getRequestPage.Method);
		}

		[Test]
		public void Should_Set_Resource()
		{
			Assert.AreEqual (string.Format("request-pages/{0}", _requestId), _getRequestPage.Resource);
		}

		[Test]
		public void Should_Use_Override_Resource()
		{
			Assert.AreEqual(new GetRequestPage("", "", "", "iamtheresource").Resource, "iamtheresource");
		}
		
		[Test]
		public void Should_Set_Bearer()
		{
			var queryParams = _getRequestPage.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Value.ToString() == string.Format("Bearer {0}+{1}", _apiId, _tokenId)));
		}

		[Test]
		public void Should_Set_Authorization_Parameter()
		{
			var queryParams = _getRequestPage.Parameters.Where (p => p.Type == ParameterType.HttpHeader);
			Assert.That(queryParams.Any(p => p.Name == "Authorization"));
		}

		[Test]
		public void Should_Set_Correct_Version()
		{
			var queryParams = _getRequestPage.Parameters.Where (p => p.Name == "v");
			Assert.That(queryParams.Any(p => p.Value == "1"));
		}

		[Test]
		public void Should_Set_Root_Element()
		{
			Assert.AreEqual(_getRequestPage.RootElement, "request_page");
		}

		[Test]
		public void Should_Accept_Typed_Json()
		{
			var queryParams = _getRequestPage.Parameters.Where (p => p.Name == "Accept");			
			Assert.That(queryParams.Any(p => p.Value == "application/typed+json"));
		}
	}
}