using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class IndicatorAttributes
    {
    	public PatientAttributes Patient {get; set;}
    	public PayerAttributes Payer {get; set;}
    	public IndicatorPrescriberAttributes Prescriber {get; set;}
    	public IndicatorPharmacyAttributes Pharmacy {get; set;}
    	public IndicatorPrescriptionAttributes Prescription {get; set;}    	
    }
}