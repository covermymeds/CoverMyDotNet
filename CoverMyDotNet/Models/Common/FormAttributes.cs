using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class FormAttributes
    {
        public int Id { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Directions { get; set; }
        public string RequestFormId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PreviewUrl { get; set; }
        public bool IsEPA { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public double Score { get; set; }
        public bool Active { get; set; }
        public bool IsEnrollment { get; set; }
    }
    public class FormAttributeList
    {
        public List<FormAttributes> Forms {get; set;}
    }
}