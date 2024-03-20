

using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListFromDb
{
    public class ListFromDbHandler : IRequestHandler<ListFromDbQuery, ErrorOr<List<ReportResult>>>
    {
        private readonly IReportService reportRepository;

        public ListFromDbHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportResult>>> Handle(ListFromDbQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var list = new List<ReportResult>();
            var reportResult =  reportRepository.GetAllReports();
            if (reportResult != null)
            {
                foreach (var report in reportResult)
                {
                    list.Add(new ReportResult(report));
                }
            }

            return list;
        }
    }
}
