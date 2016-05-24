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
	public class CreateRequestTests
	{
		private string _apiId;
		private PostRequest _postRequest;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid ().ToString ();
			_postRequest = new PostRequest (_apiId, "secret",
				new CreateRequestModel()
			);
		}
			
		[Test]
		public void Should_Set_Method()
		{
			Assert.AreEqual (Method.POST, _postRequest.Method);
		}

		[Test]
		public void Should_Set_Resource()
		{
			Assert.AreEqual ("requests", _postRequest.Resource);
		}

		[Test]
		public void Should_Set_ContentType()
		{
			Assert.AreEqual (DataFormat.Json, _postRequest.RequestFormat);
		}

		[Test]
		public void Should_Set_ApiId()
		{
			var queryParams = _postRequest.Parameters.Where (p => p.Type.ToString() == "QueryString");
			Assert.That (queryParams.Any(p => p.Name == "api_id" && p.Value.ToString() == _apiId));
		}

		[Test]
		public void Should_Set_Version()
		{
			var queryParams = _postRequest.Parameters.Where (p => p.Type.ToString() == "QueryString");
			Assert.That (queryParams.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
	}
}