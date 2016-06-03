using RestSharp;
using System.Net.Http;
using System.Net;
using System;
using System.Linq;
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
			var apiUrl = Environment.GetEnvironmentVariable("CMM_API_URL");
			this.BaseUrl = new Uri(string.IsNullOrEmpty(apiUrl) ? 
				"https://api.covermymeds.com" : apiUrl);
			//try to get the api creds from the environment first
			this.FollowRedirects = false; //we dont want to follow redirects because when we do, it does not post the correct headers
			_apiId = Environment.GetEnvironmentVariable("CMM_API_ID");
			_apiSecret = Environment.GetEnvironmentVariable("CMM_API_SECRET");		
			//for the requestpages
			AddHandler("application/typed+json", new RestSharp.Deserializers.JsonDeserializer());	
		}

		public Client(string apiId, string apiSecret, string apiUrl = "https://api.covermymeds.com") : base()
		{
			this.BaseUrl = new Uri(apiUrl);			
			this.FollowRedirects = false;
			_apiId = apiId;
			_apiSecret = apiSecret;
			//for the requestpages
			AddHandler("application/typed+json", new RestSharp.Deserializers.JsonDeserializer());				
		}
 
		public override IRestResponse<T> Execute<T>(IRestRequest request)
		{
			var resp = base.Execute<T>(request);
			if(!string.IsNullOrEmpty(resp.Content))
			{
				var errorResp = new RestSharp.Deserializers.JsonDeserializer().Deserialize<APIExceptionResponse>(resp);
				if(errorResp.Errors != null)
				{
					foreach(var v in errorResp.Errors)
					{
						var e = new Exception("The Api Returned an error response. See exception Data for more information");
						e.Data.Add("Code", v.Code);
						e.Data.Add("Message", v.Message);
						e.Data.Add("Debug", v.Debug);
						throw e;
					}
				}
			}
			return resp;
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

		public void UpdateRequest(string requestId, string tokenId, string memo)
		{
			var request = new Requests.PutRequest(_apiId, requestId, tokenId, memo);
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
		public RequestPageAttributes GetRequestPage(string requestId, string tokenId)
		{
			var request = new Requests.GetRequestPage(_apiId, requestId, tokenId);
			var resp = Execute<RequestPageAttributes>(request);
			while(resp.StatusCode == HttpStatusCode.SeeOther)
			{
				string resource = resp.Headers.First(p => p.Name == "Location").Value.ToString();
				//we only need the resource part of the URL
				resource = resource.Substring(resource.IndexOf("request-pages"), 
					resource.IndexOf("?") - resource.IndexOf("request-pages"));

				resp = Execute<RequestPageAttributes>(
					new Requests.GetRequestPage(_apiId, requestId, tokenId, resource));
			}
			return resp.Data;
		}
	}
}

