using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class PatientAttributes
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MemberId { get; set; }
        public string PBMMemberId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public AddressAttributes Address { get; set; }
    }
}

