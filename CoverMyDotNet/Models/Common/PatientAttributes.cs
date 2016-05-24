using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class PatientAttributes
    {
        public string FirstName { get; set; }
        public object MiddleName { get; set; }
        public string LastName { get; set; }
        public object MemberId { get; set; }
        public object PBMMemberId { get; set; }
        public object PhoneNumber { get; set; }
        public object Email { get; set; }
        public string DateOfBirth { get; set; }
        public object Gender { get; set; }
        public AddressAttributes Address { get; set; }
    }
}

