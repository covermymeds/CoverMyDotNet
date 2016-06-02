using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
     public class EventAttributes
    {
        public string Time { get; set; }
        public string Type { get; set; }
        public RemoteUserAttributes RemoteUser { get; set; }
    }
}