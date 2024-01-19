using Domain.Entities.User;
using System.Runtime.Serialization;

namespace Domain.Entities.Report
{
    [DataContract]
    public class Reports
    {
        [DataMember]
        public Guid ReportId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        public virtual Users? Users { get; set; }

        [DataMember]
        public string Feedback { get; set; }

        [DataMember]
        public string FeedbackBy { get; set; }
    }
}