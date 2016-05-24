using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class RequestAttributes
	{
		public bool Urgent { get; set; }
		public string FormId { get; set; }
		public string State { get; set; }
		public string Memo { get; set; }
		public PatientAttributes Patient { get; set; }
		public PayerAttributes Payer { get; set; }
		public PrescriberAttributes Prescriber { get; set; }
		public PharmacyAttributes Pharmacy { get; set; }
		public PrescriptionAttributes Prescription { get; set; }
		public EnumeratedFieldsAttributes EnumeratedFields { get; set; }
	}
}

