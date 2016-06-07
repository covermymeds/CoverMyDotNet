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
	public class GetDrugTests
	{
		private string _apiId;
		private string _drugId;
		private GetDrug _getDrug;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_drugId = Guid.NewGuid().ToString();
			_getDrug = new GetDrug(_apiId, _drugId);
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_getDrug.Method, Method.GET);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_getDrug.Resource, string.Format("drugs/{0}", _drugId));
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.AreEqual(_getDrug.RootElement, "drug");
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_getDrug.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_getDrug.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

	}
}