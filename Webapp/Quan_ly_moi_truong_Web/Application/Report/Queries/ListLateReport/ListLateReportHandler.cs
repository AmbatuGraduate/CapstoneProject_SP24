using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Application.Report.Queries.List;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Queries.ListLateReport
{
    public class ListLateReportHandler
        : IRequestHandler<ListLateReportQuery, ErrorOr<List<ReportFormatRecord>>>
    {
        private readonly IReportService reportRepository;

        public ListLateReportHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportFormatRecord>>> Handle(ListLateReportQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reports = await reportRepository.GetReportFormats(request.accessToken);
            var lateReports = reports.Where(x => x.ExpectedResolutionDate.CompareTo(DateTime.Now) <= 0).ToList();
            var results = new List<ReportFormatRecord>();

            foreach(var report in lateReports)
            {
                results.Add(new ReportFormatRecord(report));
            }
            

            return results;
        }
    }
}
