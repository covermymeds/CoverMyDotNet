using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
    public class IndicatorPrescriptionAttributes
    {
        public string DrugId { get; set; }
        public string Name { get; set; }
        public string Strength { get; set; }
        public string Frequency { get; set; }
        public int DaysSupply { get; set; }
        public int Quantity { get; set; }
        public string QuantityUnitOfMeasure { get; set; }
        public string Rationale { get; set; }
        public int Refills { get; set; }
        public bool DispenseAsWritten { get; set; }
        public DateTime DateOfService { get; set; }
    }
}

