using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class AddressAttributes
	{
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
	}
}

