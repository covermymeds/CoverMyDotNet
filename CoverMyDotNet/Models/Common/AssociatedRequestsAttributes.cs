using System;
using RestSharp.Serializers;
using System.Collections.Generic;
namespace CoverMyDotNet
{
	public class AssociatedRequestsAttributes
    {
        public List<object> Appeals { get; set; }
        public List<object> Renewals { get; set; }
        public object RequestBeingAppealed { get; set; }
        public object RequestBeingRenewed { get; set; }
    }
}