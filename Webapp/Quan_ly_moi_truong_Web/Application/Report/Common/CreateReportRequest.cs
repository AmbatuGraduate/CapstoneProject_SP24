using Domain.Enums;

namespace Application.Report.Common
{
    public class CreateReportRequest
    {
        public string IssuerEmail { get; set; }
        public string ReportSubject { get; set; }
        public string ReportBody { get; set; }
        public string IssueLocation { get; set; }
        public List<string>? ReportImages { get; set; }
        public DateTime ExpectedResolutionDate { get; set; }
        public ReportImpact ReportImpact { get; set; }
      
    }
}