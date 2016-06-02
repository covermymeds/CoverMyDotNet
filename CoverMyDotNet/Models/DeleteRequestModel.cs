using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class DeleteRequestModel
	{
		public RemoteUserAttributes RemoteUser { get; set; }
		public string TokenId { get; set; }			
	}
}

