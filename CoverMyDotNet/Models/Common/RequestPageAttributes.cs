using System;
using RestSharp.Serializers;
using System.Collections.Generic;
namespace CoverMyDotNet
{
    public class RequestPageAttributes
    {
		public List<FormQuestionAttributes> Forms { get; set; }
	    public List<ActionAttributes> Actions { get; set; }
	    public List<ProvidedCodedReferenceAttributes> ProvidedCodedReferences { get; set; }
	    public List<ValidationAttributes> Validations { get; set; }
    }
}