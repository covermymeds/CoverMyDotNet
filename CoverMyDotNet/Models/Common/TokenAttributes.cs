	using System;
using RestSharp.Serializers;

namespace CoverMyDotNet
{
	public class TokenAttributes
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string Href { get; set; }
        public string HtmlUrl { get; set; }
        public string PdFUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}