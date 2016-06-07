using System;
using System.Collections.Generic;

namespace CoverMyDotNet
{
	public class APIExceptionResponse
	{
		public List<APIException> Errors {get; set;}
	}
	public class APIException
	{
		public int Code {get; set;}
		public string Message {get; set;}
		public string Debug {get; set;}
	}
}