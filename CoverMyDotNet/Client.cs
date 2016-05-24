using RestSharp;
using System.Net.Http;
using System;

namespace CoverMyDotNet
{
	public class Client : RestClient
	{
		private string _apiId;
		private string _apiSecret;

		public string ApiId { get { return _apiId; }}
		public string ApiSecret { get { return _apiSecret; }}

		public Client() : base()
		{
			this.BaseUrl = new Uri("https://api.covermymeds.com");
			//try to get the api creds from the environment first
			_apiId = Environment.GetEnvironmentVariable("cmm_api_id");
			_apiSecret = Environment.GetEnvironmentVariable("cmm_api_secret");			
		}

		public Client(string apiId, string apiSecret) : base()
		{
			this.BaseUrl = new Uri("https://api.covermymeds.com");			
			_apiId = apiId;
			_apiSecret = apiSecret;
		}

		public ResponseAttributes CreateRequest(RequestAttributes requestData)
		{
			var request = new Requests.PostRequest(_apiId, _apiSecret, new CreateRequestModel()
			{
				Request = requestData
			});
			return Execute<ResponseAttributes>(request).Data;
		}
		
		public ResponseAttributes GetRequest(string requestId, string tokenId)
		{
			var request = new Requests.GetRequest(_apiId, tokenId, requestId);
			return Execute<ResponseAttributes>(request).Data;
		}

		public ResponseListAttributes GetRequests(string[] tokens)
		{
			var request = new Requests.GetRequests(_apiId, tokens);
			return Execute<ResponseListAttributes>(request).Data;
		}

		public void UpdateResponse(string requestId, string tokenId, string memo)
		{
			var request = new Requests.PutRequest(_apiId, requestId, TokenId, memo);
			Execute(request);
		}

		public void DeleteResponse(string requestId, string tokenId, RemoteUserAttributes remoteUser)
		{
			var request = new Requests.DeleteRequest(_apiId, tokenId, requestId, new DeleteRequestModel()
			{
				RemoteUser = remoteUser,
				TokenId = tokenId
			});
			Execute(request);
		}
	}
}

