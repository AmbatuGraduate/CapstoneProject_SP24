using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Common
{
    public class CreateReportRequest
    {
        public string IssuerEmail {  get; set; }
        public string ReportSubject { get; set; }
        public string ReportBody { get; set; }
        public DateTime ExpectedResolutionDate { get; set; }
        public ReportImpact ReportImpact {  get; set; }
    }
}
