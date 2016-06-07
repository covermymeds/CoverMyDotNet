using System;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class SearchIndicator : RestRequest
	{
		public SearchIndicator(string apiId, IndicatorSearchAttributes indicator) : base()
		{
			this.JsonSerializer = new CoverMyDotNet.JsonSerializer ();
			this.Method = Method.POST;
			Resource = "indicators/search";
			this.AddJsonBody(indicator);
			this.AddQueryParameter ("v", "1");
			this.AddParameter("Authorization", string.Format("Bearer {0}+x-no-pass", apiId), ParameterType.HttpHeader);
		}
	}
}