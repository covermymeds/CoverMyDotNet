using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class FormQuestionAttributes
	{
	    public string Identifier { get; set; }
	    public List<QuestionSetAttributes> QuestionSets { get; set; }
	}
	public class QuestionSetAttributes
	{
	    public string Title { get; set; }
	    public List<QuestionAttributes> Questions { get; set; }
	}

}

