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
	public class SearchDrugTests
	{
		private string _apiId;
		private string _query;
		private SearchDrugs _searchDrug;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_query = Guid.NewGuid().ToString();
			_searchDrug = new SearchDrugs(_apiId, _query);
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_searchDrug.Method, Method.GET);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_searchDrug.Resource, "drugs");
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.AreEqual(_searchDrug.RootElement, null);
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_searchDrug.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_searchDrug.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

		[Test]
		public void Should_Have_Correct_Parameters()
		{
			Assert.That(_searchDrug.Parameters.Any(p => p.Name == "q" && p.Value.ToString() == _query));
		}

	}
}