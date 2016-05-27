using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class GetDrug : RestRequest
	{
		public GetDrug(string apiId, string id) : base()
		{
			this.Method = Method.GET;
			Resource = string.Format("drugs/{0}", id);
			RootElement = "drug";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}