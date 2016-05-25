using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class CreateRequestModel
	{
		public RemoteUserAttributes RemoteUser { get; set; }
		public RequestAttributes Request { get; set; }			
	}
}

