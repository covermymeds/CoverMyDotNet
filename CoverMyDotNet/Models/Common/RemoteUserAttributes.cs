using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class RemoteUserAttributes
    {
        public string DisplayName {get; set;}
        public string PhoneNumber {get; set;}
        public string FaxNumber {get; set;}
    }
}