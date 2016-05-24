using System;
using RestSharp.Serializers;
namespace CoverMyDotNet
{
    public class AuthorizationPeriodAttributes
    {
        public DateTime AuthorizationPeriod {get; set;}
        public DateTime ExpirationDate {get; set;}
    }    
}