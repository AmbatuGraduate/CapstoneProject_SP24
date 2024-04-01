using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, ErrorOr<ReportFormatRecord>>
    {
        private readonly IReportService reportRepository;

        public GetByIdHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportResult = await reportRepository.GetReportById(request.accessToken, request.id);

            return new ReportFormatRecord(reportResult);
        }
    }
}