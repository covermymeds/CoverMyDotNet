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
	public class PostIndicatorTests
	{
		private string _apiId;
		private PostIndicator _postIndicator;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_postIndicator = new PostIndicator(_apiId, new IndicatorAttributes());
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_postIndicator.Method, Method.POST);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_postIndicator.Resource, "indicators");
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.AreEqual(_postIndicator.RootElement, "indicator");
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_postIndicator.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_postIndicator.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

	}
}