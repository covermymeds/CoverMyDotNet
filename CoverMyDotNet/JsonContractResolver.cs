﻿using System;
using Newtonsoft.Json.Serialization;

namespace CoverMyDotNet
{
	public class JsonContractResolver : DefaultContractResolver 
	{
		protected override string ResolvePropertyName(string propertyName)
		{
			return System.Text.RegularExpressions.Regex.Replace(
				propertyName, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower(); 
		}
	}
}

