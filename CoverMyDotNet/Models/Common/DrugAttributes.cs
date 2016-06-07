using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class DrugListAttributes
    {
        public List<DrugAttributes> Drugs{get; set;}
    }

    public class DrugAttributes
    {
        public string Id { get; set; }
        public string GPI { get; set; }
        public object SortGroup { get; set; }
        public object SortOrder { get; set; }
        public string Name { get; set; }
        public string RouteOfAdministration { get; set; }
        public string DosageForm { get; set; }
        public string Strength { get; set; }
        public string StrengthUnitOfMeasure { get; set; }
        public string DosageNormName { get; set; }
        public string FullName { get; set; }
        public string Href { get; set; }
    }
}