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
        public object Strength { get; set; }
        public object Frequency { get; set; }
        public object DaysSupply { get; set; }
        public string Quantity { get; set; }
        public object Quantity_unit_of_measure { get; set; }
        public object Rationale { get; set; }
        public object Refills { get; set; }
        public object DispenseAsWritten { get; set; }
        public object DateOfService { get; set; }
    }
}

