using System;
using System.Text;
using RestSharp;

namespace CoverMyDotNet.Requests
{
	public class DeleteToken : RestRequest
	{
		public DeleteToken(string apiId, string apiSecret, string tokenId)
		{
			this.Method = Method.DELETE;
			this.Resource = string.Format("requests/tokens/{0}", tokenId);
			this.AddQueryParameter("v", "1");
			this.AddParameter("Authorization", "Basic " + Convert.ToBase64String(
				Encoding.ASCII.GetBytes(string.Format("{0}:{1}", apiId, apiSecret))), 
					ParameterType.HttpHeader);
		}
	}
}