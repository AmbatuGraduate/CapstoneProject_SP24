using Domain.Enums;

namespace Application.Report.Common
{
    public class ReponseReportRequest
    {
        public string ReportID { get; set; }
        public string Response { get; set; }
        public ReportStatus Status { get; set; }
    }
}