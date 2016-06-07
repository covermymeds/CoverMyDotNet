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
	public class GetFormTests
	{
		private string _apiId;
		private string _formId;
		private GetForm _getForm;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_formId = Guid.NewGuid().ToString();
			_getForm = new GetForm(_apiId, _formId);
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_getForm.Method, Method.GET);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_getForm.Resource, string.Format("forms/{0}", _formId));
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.AreEqual(_getForm.RootElement, "form");
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_getForm.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_getForm.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

	}
}