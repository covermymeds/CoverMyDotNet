using System.Net.Http;
using NUnit.Framework;
using Moq;
using System;
using RestSharp;
using System.Linq;

namespace CoverMyDotNet.Tests
{
	[TestFixture]
	public class ClientTests
	{
		private Client _client;
		private string _apiId;
		private string _apiSecret;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_apiSecret = Guid.NewGuid().ToString();
			_client = new Client (_apiId, _apiSecret);
		}

		[Test]
		public void Should_Default_Host()
		{
			Assert.AreEqual ("https://api.covermymeds.com/", _client.BaseUrl.ToString ());
		}

		[Test]
		public void Should_HaveSetApiId()
		{
			Assert.AreEqual(_apiId, _client.ApiId);
		}

		[Test]
		public void Should_HaveSetApiSecret()
		{
			Assert.AreEqual(_apiSecret, _client.ApiSecret);
		}
	}
}

