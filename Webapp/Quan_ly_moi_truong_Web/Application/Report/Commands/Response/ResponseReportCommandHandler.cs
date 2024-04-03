using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Response
{
    public class ResponseReportCommandHandler : IRequestHandler<ReponseReportCommand, ErrorOr<ReportFormatRecord>>
    {
        private readonly IReportService reportRepository;



        public ResponseReportCommandHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;


        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(ReponseReportCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportResult = await reportRepository.ReponseReport(request.AccessToken, request.ReportID, request.Response, request.Status);



            return new ReportFormatRecord(reportResult);
        }
    }
}