using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListLateReport
{
    public class ListLateReportHandler
        : IRequestHandler<ListLateReportQuery, ErrorOr<List<ReportResult>>>
    {
        private readonly IReportService reportRepository;

        public ListLateReportHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportResult>>> Handle(ListLateReportQuery request, CancellationToken cancellationToken)
        {
            var reports = reportRepository.GetAllReports();
            var lateReports = reports.Where(x => x.ExpectedResolutionDate.CompareTo(DateTime.Now) <= 0 && x.Status == ReportStatus.UnResolved).ToList();
            var results = new List<ReportResult>();

            foreach (var report in lateReports)
            {
                results.Add(new ReportResult(report));
            }

            return results;
        }
    }
}