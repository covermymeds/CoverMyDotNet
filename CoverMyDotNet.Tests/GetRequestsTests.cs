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
	public class GetRequestsTests
	{
		private string _apiId;
		private GetRequests _getRequests;
		private string[] _tokenIds = new String[5];

		[SetUp]
		public void Setup()
		{
			for(int i = 0;i<5;i++)
				_tokenIds[i] = Guid.NewGuid ().ToString ();
			_apiId = Guid.NewGuid ().ToString ();
			_getRequests = new GetRequests(_apiId, _tokenIds);
		}

		[Test]
		public void Should_Be_Get_Request()
		{
			Assert.AreEqual(_getRequests.Method, Method.GET);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_getRequests.Resource, "requests");
		}

		[Test]
		public void Should_Have_Correct_Version()
		{
			var queryParams = _getRequests.Parameters.Where(p => p.Name == "v");
			Assert.That(queryParams.Any(p => p.Value.ToString() == "1"));
		}

		[Test]
		public void Should_Use_Correct_Root_Element()
		{
			Assert.AreEqual(_getRequests.RootElement, "request");
		}

		[Test]
		public void Should_Have_Correct_Authorization()
		{
			var queryParams = _getRequests.Parameters.Where(p => p.Name == "Authorization");
			Assert.That(queryParams.Any(p => 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

		[Test]
		public void Should_Have_Correct_Token_Params()
		{
			var queryParams = _getRequests.Parameters.Where(p => p.Name == "token_ids[]");
			foreach(var s in _tokenIds)
				Assert.That(queryParams.Any(p => p.Value.ToString() == s));
		}
	}
}