

using Application.Common.Interfaces.Persistence;
using Application.Report.Common;
using Domain.Entities.Report;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Create
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, ErrorOr<ReportFormatRecord>>
    {
        private readonly IReportService reportRepository;

        public CreateReportCommandHandler(IReportService reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            ReportFormat createReport = new ReportFormat
            {
                AccessToken = request.AccessToken,
                IssuerEmail = request.IssuerEmail,
                ReportSubject = request.ReportSubject,
                ReportBody = request.ReportBody
            };

            // add to google
            var reportResult = await reportRepository.CreateReport(createReport);

            // add to db
            reportRepository.AddReport(new Reports
            {
                ReportId = reportResult.Id,
                IssuerGmail = request.IssuerEmail,
            });

            return new ReportFormatRecord(reportResult);
        }
    }
}