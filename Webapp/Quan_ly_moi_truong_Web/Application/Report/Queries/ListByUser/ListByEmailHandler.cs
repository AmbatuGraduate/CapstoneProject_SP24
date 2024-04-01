using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.ListByUser
{
    public class ListByEmailHandler : IRequestHandler<ListByEmailQuery, ErrorOr<List<ReportFormatRecord>>>
    {
        private readonly IReportService reportRepository;

        public ListByEmailHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<List<ReportFormatRecord>>> Handle(ListByEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportResult = await reportRepository.GetReportsByUser(request.accessToken, request.email);
            var list = new List<ReportFormatRecord>();
            foreach (var report in reportResult)
            {
                list.Add(new ReportFormatRecord(report));
            }
            return new List<ReportFormatRecord>(list);
        }
    }
}