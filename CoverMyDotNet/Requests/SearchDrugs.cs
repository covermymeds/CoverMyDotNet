using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class SearchDrugs : RestRequest
	{
		public SearchDrugs(string apiId, string query) : base()
		{
			this.Method = Method.GET;
			Resource = "drugs";
			this.AddQueryParameter ("v", "1");
			this.AddQueryParameter ("q", query);
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}