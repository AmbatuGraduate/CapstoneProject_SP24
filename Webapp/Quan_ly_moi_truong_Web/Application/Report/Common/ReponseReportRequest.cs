using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Common
{
    public class ReponseReportRequest
    {
        public string ReportID { get; set; }
        public string Response {  get; set; }
        public ReportStatus Status {  get; set; }
    }
}
