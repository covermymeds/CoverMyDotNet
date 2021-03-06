﻿using RestSharp;
using System.Net.Http;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;

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

		public IRestResponse DeleteResponse(string requestId, string tokenId, RemoteUserAttributes remoteUser)
		{
			var request = new Requests.DeleteRequest(_apiId, tokenId, requestId, new DeleteRequestModel()
			{
				RemoteUser = remoteUser,
				TokenId = tokenId
			});
			return Execute(request);
		}

		public IRestResponse<RequestPageAttributes> GetRequestPage(string requestId, string tokenId)
		{
			var request = new Requests.GetRequestPage(_apiId, _apiSecret, requestId, tokenId);
			return Execute<RequestPageAttributes>(request);
		}

		public DrugListAttributes SearchDrugs(string query)
		{
			var request = new Requests.SearchDrugs(_apiId, query);
			return Execute<DrugListAttributes>(request).Data;
		}

		public DrugAttributes GetDrug(string id)
		{
			var request = new Requests.GetDrug(_apiId, id);
			return Execute<DrugAttributes>(request).Data;
		}

		public FormAttributes GetForm(string id)
		{
			var request = new Requests.GetForm(_apiId, id);
			return Execute<FormAttributes>(request).Data;
		}

		public FormAttributeList SearchForms(string drugId, string state, string threshold, 
			 string bin, string pcn, string groupId)
		{
			var request = new Requests.SearchForm(_apiId, drugId, state, threshold, bin, pcn, groupId);
			return Execute<FormAttributeList>(request).Data;
		}
			public FormAttributeList SearchForms(string drugId, string state, string q)
		{
			var request = new Requests.SearchForm(_apiId, drugId, state, q);
			return Execute<FormAttributeList>(request).Data;
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

