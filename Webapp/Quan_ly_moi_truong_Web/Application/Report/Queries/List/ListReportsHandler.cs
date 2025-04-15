using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.List
{
    public class ListReportsHandler : IRequestHandler<ListReportsQuery, ErrorOr<List<ReportFormatRecord>>>
    {
        private readonly IReportService reportRepository;

        public ListReportsHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportFormatRecord>>> Handle(ListReportsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportResult = await reportRepository.GetReportFormats(request.accessToken);
            var list = new List<ReportFormatRecord>();
            foreach (var report in reportResult)
            {
                list.Add(new ReportFormatRecord(report));
            }
            return new List<ReportFormatRecord>(list);
        }
    }
}