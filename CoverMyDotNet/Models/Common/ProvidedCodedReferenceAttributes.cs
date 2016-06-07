using System;
using RestSharp.Serializers;
using System.Collections.Generic;
namespace CoverMyDotNet
{
    public class ProvidedCodedReferenceAttributes
    {
    	public string Identifier { get; set; }
	    public string Href { get; set; }
	    public string Method { get; set; }
	    public List<DataFieldAttributes> DataFields { get; set; }
    }

    public class DataFieldAttributes
    {
       public string QuestionId { get; set; }
       public string QueryParameter { get; set; }
    }
}