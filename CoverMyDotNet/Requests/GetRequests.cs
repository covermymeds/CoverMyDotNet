using System;
using RestSharp;
using System.Collections.Generic;

namespace CoverMyDotNet.Requests
{
	public class GetRequests : RestRequest
	{
		public GetRequests(string apiId, string[] tokens) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			this.Method = Method.GET;
			Resource = "requests";
			RequestFormat = DataFormat.Json;
			var tokenDict = new Dictionary<string, string[]>();
			tokenDict.Add("token_ids", tokens);
			this.AddJsonBody (tokenDict);
			RootElement = "request";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}