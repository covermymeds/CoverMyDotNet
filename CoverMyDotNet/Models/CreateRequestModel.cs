using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class CreateRequestModel
	{
		public ForeignUserAttributes ForeignUserAttributes { get; set; }
		public RequestAttributes Request { get; set; }			
	}
}

