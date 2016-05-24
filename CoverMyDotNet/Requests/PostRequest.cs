using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class PostRequest : RestRequest
	{
		public PostRequest(string apiId, string secret, CreateRequestModel model) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			Method = Method.POST;
			Resource = "requests";
			RequestFormat = DataFormat.Json;
			this.AddJsonBody (model);
			RootElement = "request";
			this.AddQueryParameter ("v", "1");
			this.AddQueryParameter ("api_id", apiId);
		}
	}
}

