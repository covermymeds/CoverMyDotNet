using System;
using RestSharp.Serializers;
using System.Collections.Generic;
namespace CoverMyDotNet
{
	public class AssociatedRequestsAttributes
    {
    	public class Request
    	{
    		public string Url {get; set;}
    		public string Href {get; set;}
    	}
        public List<Request> Appeals { get; set; }
        public List<Request> Renewals { get; set; }
        public Request RequestBeingAppealed { get; set; }
        public Request RequestBeingRenewed { get; set; }
    }
}