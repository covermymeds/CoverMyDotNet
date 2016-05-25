using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class PutRequest : RestRequest
	{
		public PutRequest(string apiId, string requestId, string tokenId, string memo) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			Method = Method.PUT;
			Resource = string.Format("requests/{0}", requestId);
			this.AddJsonBody (new CreateRequestModel()
				{
					Request = new RequestAttributes()
					{
						Memo = memo						
					}
				});
			RootElement = "request";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+{1}", apiId, tokenId), ParameterType.HttpHeader);
		}
	}
}

