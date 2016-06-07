using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class SearchForm : RestRequest
	{
		private void Init(string apiId)
		{
			this.Method = Method.GET;
			Resource = "forms";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
		public SearchForm(string apiId, string drugId, string state, string threshold, 
			 string bin, string pcn, string groupId) : base()
		{
			this.Init(apiId);
			this.AddQueryParameter("drug_id", drugId);
			this.AddQueryParameter("state", state);
			this.AddQueryParameter("bin", bin);
			this.AddQueryParameter("pcn", pcn);
			this.AddQueryParameter("group_id", groupId);
			this.AddQueryParameter("threshold", threshold);
		}

		public SearchForm(string apiId, string drugId, string state, string q) : base()
		{
			this.Init(apiId);
			this.AddQueryParameter("drug_id", drugId);
			this.AddQueryParameter("state", state);
			this.AddQueryParameter("q", q);
		}
	}
}