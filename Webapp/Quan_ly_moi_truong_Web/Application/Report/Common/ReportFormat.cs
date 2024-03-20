

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
        public ReportStatus ReportStatus { get; set; }
        public string ReportResponse { get; set; }
    }
}