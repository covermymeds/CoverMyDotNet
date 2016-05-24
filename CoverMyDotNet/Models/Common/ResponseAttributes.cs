using System;
using RestSharp.Serializers;
using System.Collections.Generic;

namespace CoverMyDotNet
{
    public class ResponseListAttributes
    {
        public List<ResponseAttributes> Requests{get; set;}
    }

    public class ResponseAttributes
    {
        public string Memo { get; set; }
        public string ApiId { get; set; }
        public bool isStale { get; set; }
        public AssociatedRequestsAttributes AssociatedRequests { get; set; }
        public string Id { get; set; }
        public string FormId { get; set; }
        public string OriginalFormId { get; set; }
        public PatientAttributes Patient { get; set; }
        public PharmacyAttributes Pharmacy { get; set; }
        public PayerAttributes Payer { get; set; }
        public PrescriberAttributes Prescriber { get; set; }
        public PrescriptionAttributes Prescription { get; set; }
        public EnumeratedFieldsAttributes EnumeratedFields { get; set; }
        public bool isEPA { get; set; }
        public string Href { get; set; }
        public object PlanOutcome { get; set; }
        public AuthorizationPeriodAttributes AuthorizationPeriod { get; set; }
        public string WorkflowStatus { get; set; }
        public List<string> ThumbnailUrls { get; set; }
        public string PDFUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string CreatedAt { get; set; }
        public string State { get; set; }
        public bool Urgent { get; set; }
        public int AttachmentsIncluded { get; set; }
        public string ResponseFromPlan { get; set; }
        public bool isAppeal { get; set; }
        public bool isRenewal { get; set; }
        public object DeadlineForReply { get; set; }
        public List<TokenAttributes> Tokens { get; set; }
        public List<EventAttributes> Events { get; set; }
        public List<object> Attachments { get; set; }
    }

    public class FullResponse
    {
        RemoteUserAttributes RemoteUser {get; set;}
        RequestAttributes Request {get; set;}
    }
}