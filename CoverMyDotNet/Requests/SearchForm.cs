using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class SearchForm : RestRequest
	{
		public SearchForm(string apiId, string drugId, string state, string threshold, 
			 string bin, string pcn, string groupId) : this(apiId, drugId, state)
		{
			this.AddQueryParameter("drug_id", drugId);
			this.AddQueryParameter("state", state);
			this.AddQueryParameter("bin", bin);
			this.AddQueryParameter("pcn", pcn);
			this.AddQueryParameter("groupId", groupId);
			this.AddQueryParameter("threshold", threshold);
		}

		public SearchForm(string apiId, string drugId, string state, string q = "") : base()
		{
			this.Method = Method.GET;
			Resource = "forms";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
			this.AddQueryParameter("drug_id", drugId);
			this.AddQueryParameter("state", state);
			if(q != "")
				this.AddQueryParameter("q", q);
		}
	}
}