using System;
using System.Text;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class GetRequestPage : RestRequest
	{
		public GetRequestPage(string apiId, string requestId, string tokenId, string resource = "")
		{
			this.Method = Method.GET;
			this.Resource = string.IsNullOrEmpty(resource) ? 
								string.Format("request-pages/{0}", requestId) : resource;
			this.RootElement = "request_page";
			this.AddQueryParameter("v", "1");
			this.AddParameter("Accept", "application/typed+json", ParameterType.HttpHeader);
			this.AddParameter("Authorization", string.Format("Bearer {0}+{1}", apiId, tokenId), ParameterType.HttpHeader);			
		}
	}
}