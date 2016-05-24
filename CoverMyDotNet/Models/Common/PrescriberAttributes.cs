using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class PrescriberAttributes
    {
        public string NPI { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public object ClinicName { get; set; }
        public object FaxNumber { get; set; }
        public object PhoneNumber { get; set; }
        public AddressAttributes Address { get; set; }
    }
}

