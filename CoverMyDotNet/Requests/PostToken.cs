using System;
using System.Text;
using RestSharp;
using System.Collections.Generic;

namespace CoverMyDotNet.Requests
{
	public class PostToken : RestRequest
	{
		public PostToken(string apiId, string apiSecret, string[] requestIds)
		{
			this.Method = Method.POST;
			this.Resource = "requests/tokens";
			this.AddQueryParameter("v", "1");
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			var body = new Dictionary<string, string[]>();
			body.Add("request_ids", requestIds);
			this.AddJsonBody(body);
			this.AddParameter("Authorization", "Basic " + Convert.ToBase64String(
				Encoding.ASCII.GetBytes(string.Format("{0}:{1}", apiId, apiSecret))), 
					ParameterType.HttpHeader);
		}
	}
}