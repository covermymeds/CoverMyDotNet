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
	public class SearchIndicatorTests
	{
		private string _apiId;
		private SearchIndicator _searchIndicator;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_searchIndicator = new SearchIndicator(_apiId, new IndicatorSearchAttributes());
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_searchIndicator.Method, Method.POST);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_searchIndicator.Resource, "indicators/search"); 
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.AreEqual(_searchIndicator.RootElement, null);
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_searchIndicator.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_searchIndicator.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

	}
}