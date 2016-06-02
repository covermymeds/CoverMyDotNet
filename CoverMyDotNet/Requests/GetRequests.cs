using System;
using RestSharp;
using System.Collections.Generic;

namespace CoverMyDotNet.Requests
{
	public class GetRequests : RestRequest
	{
		public GetRequests(string apiId, string[] tokens) : base()
		{
			this.Method = Method.GET;
			Resource = "requests";	
			foreach (string s in tokens)
				this.AddQueryParameter ("token_ids[]", s);	
			RootElement = "request";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}