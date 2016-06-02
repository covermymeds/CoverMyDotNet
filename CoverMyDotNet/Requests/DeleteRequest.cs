using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class DeleteRequest : RestRequest
	{
		public DeleteRequest(string apiId, string tokenId, string requestId, DeleteRequestModel deleteRequest) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			this.Method = Method.DELETE;
			Resource = string.Format("requests/{0}", requestId);
			this.AddJsonBody(deleteRequest);
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+{1}", apiId, tokenId), ParameterType.HttpHeader);
		}
	}
}