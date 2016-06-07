using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class GetForm : RestRequest
	{
		public GetForm(string apiId, string formId) : base()
		{
			this.Method = Method.GET;
			Resource = string.Format("forms/{0}", formId);
			RootElement = "form";
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}