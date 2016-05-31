using System;

namespace CoverMyDotNet
{
	public class IndicatorPharmacyAttributes : PharmacyAttributes
	{
		public string FaxNumber {get; set;}
		public AddressAttributes Address {get; set;}
	}
}