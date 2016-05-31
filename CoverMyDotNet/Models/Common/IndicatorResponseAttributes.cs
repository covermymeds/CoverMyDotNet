using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class IndicatorResponsePrescriptionAttributes : PrescriptionAttributes
	{
		public bool PARequired {get; set;}
		public bool Autostart {get; set;}
		public bool Predicted {get; set;}
		public string Message {get; set;}
		public string SponsoredMessage {get; set;}
		public decimal CoPayAmt {get; set;}
		public bool DrugSubstitutionPerformed {get; set;}  
	}

	public class IndicatorResponseAttributes
	{
		public RequestAttributes Request {get; set;}
		public PatientAttributes Patient {get; set;}
    	public PayerAttributes Payer {get; set;}
    	public IndicatorResponsePrescriptionAttributes Prescription {get; set;}
    	public PharmacyAttributes PharmacyAttributes {get; set;}
	}
}