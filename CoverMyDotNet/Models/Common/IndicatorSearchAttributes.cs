using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class IndicatorSearchAttributes
    {
    	public PatientAttributes Patient {get; set;}
    	public PayerAttributes Payer {get; set;}
    	public IndicatorPrescriberAttributes Prescriber {get; set;}
    	public IndicatorPharmacyAttributes Pharmacy {get; set;}
    	public List<IndicatorPrescriptionAttributes> Prescriptions {get; set;}    
    }
    public class IndicatorSearchResultAttributes
    {
    	public PatientAttributes Patient {get; set;}
    	public PayerAttributes Payer {get; set;}
    	public IndicatorPrescriberAttributes Prescriber {get; set;}
    	public IndicatorPharmacyAttributes Pharmacy {get; set;}
    	public List<IndicatorResponsePrescriptionAttributes> Prescriptions {get; set;}    
    	
    }
}