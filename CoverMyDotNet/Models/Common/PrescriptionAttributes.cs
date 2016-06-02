using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
    public class PrescriptionAttributes
    {
        public string DrugId { get; set; }
        public string NDC { get; set; }
        public object PrescriptionReference { get; set; }
        public string Name { get; set; }
        public string Strength { get; set; }
        public string Frequency { get; set; }
        public int DaysSupply { get; set; }
        public float Quantity { get; set; }
        public string Quantity_unit_of_measure { get; set; }
        public string Rationale { get; set; }
        public int Refills { get; set; }
        public bool DispenseAsWritten { get; set; }
        public DateTime DateOfService { get; set; }
    }
}

