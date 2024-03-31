using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Application.Report.Queries.ListLateReport;
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Queries.ListUnresolve
{
    public class ListUnresolveReportHandler
        : IRequestHandler<ListUnresolveReportQuery, ErrorOr<List<ReportResult>>>
    {
        private readonly IReportService reportRepository;

        public ListUnresolveReportHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportResult>>> Handle(ListUnresolveReportQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reports = reportRepository.GetAllReports();
            var unresolveReports = reports.Where(x => x.Status == ReportStatus.UnResolved).ToList();
            var results = new List<ReportResult>();

            foreach (var report in unresolveReports)
            {
                results.Add(new ReportResult(report));
            }


            return results;
        }
    }
}
