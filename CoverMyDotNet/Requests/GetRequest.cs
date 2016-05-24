using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class GetRequest : RestRequest
	{
		public GetRequest(string apiId, string tokenId, string requestId) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			this.Method = Method.GET;
			Resource = string.Format("requests/{0}", requestId);
			RequestFormat = DataFormat.Json;
			RootElement = "request";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+{1}", apiId, tokenId), ParameterType.HttpHeader);
		}
	}
}