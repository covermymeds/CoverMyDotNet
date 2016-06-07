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
	public class SearchFormsTests
	{
		private string _apiId;
		private string _drugId;
		private string _state;
		private string _bin;
		private string _pcn;
		private string _groupId;
		private string _threshold;

		private string _query;

		private SearchForm _searchFormSimple;
		private SearchForm _searchFormComplex;

		[SetUp]
		public void Setup()
		{
			_apiId = Guid.NewGuid().ToString();
			_drugId = Guid.NewGuid().ToString();
			_state = Guid.NewGuid().ToString();
			_bin = Guid.NewGuid().ToString();
			_pcn = Guid.NewGuid().ToString();
			_groupId = Guid.NewGuid().ToString();
			_threshold = Guid.NewGuid().ToString();
			_query = Guid.NewGuid().ToString();

			_searchFormSimple = new SearchForm(_apiId, _drugId, _state, _query);
			_searchFormComplex = new SearchForm(_apiId, _drugId, _state, _threshold,
				 _bin, _pcn, _groupId);
		}

		[Test]
		public void Should_Use_Correct_Method()
		{
			Assert.AreEqual(_searchFormSimple.Method, Method.GET);
			Assert.AreEqual(_searchFormComplex.Method, Method.GET);
		}

		[Test]
		public void Should_Use_Correct_Resource()
		{
			Assert.AreEqual(_searchFormSimple.Resource, "forms");
			Assert.AreEqual(_searchFormComplex.Resource, "forms");
		}

		[Test]
		public void Should_Use_Correct_RootElement()
		{
			Assert.IsNull(_searchFormSimple.RootElement);
			Assert.IsNull(_searchFormComplex.RootElement);
		}

		[Test]
		public void Should_Have_Version_Parameter()
		{
			Assert.That(_searchFormSimple.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "v" && p.Value.ToString() == "1"));

		}
		
		[Test]
		public void Should_Have_Authorization_Parameter()
		{
			Assert.That(_searchFormSimple.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "Authorization" && 
				p.Value.ToString() == string.Format("Bearer {0}+x-no-pass", _apiId)));
		}

		[Test]
		public void Should_Have_Correct_Query_Params()
		{
			Assert.That(_searchFormSimple.Parameters.Any(p => p.Name == "drug_id" && p.Value.ToString() == _drugId));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "drug_id" && p.Value.ToString() == _drugId));

			Assert.That(_searchFormSimple.Parameters.Any(p => p.Name == "state" && p.Value.ToString() == _state));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "state" && p.Value.ToString() == _state));

			Assert.That(_searchFormSimple.Parameters.Any(p => p.Name == "q" && p.Value.ToString() == _query));

			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "bin" && p.Value.ToString() == _bin));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "pcn" && p.Value.ToString() == _pcn));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "group_id" && p.Value.ToString() == _groupId));
			Assert.That(_searchFormComplex.Parameters.Any(p => p.Name == "threshold" && p.Value.ToString() == _threshold));

		}
	}
}