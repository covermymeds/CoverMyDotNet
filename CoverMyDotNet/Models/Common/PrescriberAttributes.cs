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
        public string ClinicName { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public AddressAttributes Address { get; set; }
    }
}

