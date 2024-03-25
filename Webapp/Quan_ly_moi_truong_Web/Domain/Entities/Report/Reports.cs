using Domain.Enums;
using System.Runtime.Serialization;

namespace Domain.Entities.Report
{
    [DataContract]
    public class Reports
    {
        [DataMember]
        public string ReportId { get; set; }

        [DataMember]
        public string IssuerGmail { get; set; }

        [DataMember]
        public ReportStatus Status { get; set; }

        [DataMember]
        public ReportImpact ReportImpact { get; set; }

        [DataMember]
        public DateTime ExpectedResolutionDate { get; set; }

        [DataMember]
        public DateTime ActualResolutionDate { get; set; }

        [DataMember]
        public string ResponseId { get; set; }
    }
}