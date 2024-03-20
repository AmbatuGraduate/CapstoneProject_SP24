using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
    }
}
