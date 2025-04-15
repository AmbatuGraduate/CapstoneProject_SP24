using Domain.Enums;

namespace Application.Report.Common
{
    public class ReportFormat
    {
        public string AccessToken { get; set; }
        public string Id { get; set; }
        public string IssuerEmail { get; set; }
        public string ReportSubject { get; set; }
        public string ReportBody { get; set; }
        public List<string>? ReportImages { get; set; }
        public string ReportStatus { get; set; }
        public ReportImpact ReportImpact { get; set; }
        public DateTime ExpectedResolutionDate { get; set; }
        public DateTime? ActualResolutionDate { get; set; }
        public string ReportResponse { get; set; }
        public string IssueLocation { get; set; }
    }
}