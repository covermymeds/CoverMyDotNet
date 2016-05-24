using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{	
    public class PayerAttributes
    {
        public string FormSearchText { get; set; }
        public string Bin { get; set; }
        public string PCN { get; set; }
        public string GroupId { get; set; }
        public string MedicalBenefitName { get; set; }
        public string DrugBenefitName { get; set; }
    }

}

