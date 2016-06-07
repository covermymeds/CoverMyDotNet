using System;
using RestSharp.Serializers;
using System.Collections.Generic;
namespace CoverMyDotNet 
{
	public class CodedReferenceAttributes
	{
	    public string Qualifier { get; set; }
	    public string Code { get; set; }
	    public string CodeSystemVersion { get; set; }
	    public object Id { get; set; }
	    public string Ref { get; set; }
	}
}