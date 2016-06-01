using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class QuestionAttributes
    {
        public string QuestionType { get; set; }
        public string QuestionId { get; set; }
        public string DefaultNextQuestionId { get; set; }
        public string QuestionText { get; set; }
        public object HelpText { get; set; }
        public string ContentPlain { get; set; }
        public string ContentHtml { get; set; }
        public CodedReferenceAttributes CodedReference { get; set; }
        public string QuestionAnswer { get; set; }
        public object Flag { get; set; }
        public List<object> Validations { get; set; }
        public object Placeholder { get; set; }
    }
}