

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

            var reportDbId = Guid.NewGuid().ToString();
            ReportFormat createReport = new ReportFormat
            {
                Id = reportDbId,
                AccessToken = request.AccessToken,
                IssuerEmail = request.IssuerEmail,
                ReportSubject = request.ReportSubject,
                ReportBody = request.ReportBody,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ReportImpact = request.ReportImpact
            };

            // add to google
            var reportResult = await reportRepository.CreateReport(createReport);

            // add to db
           
            reportRepository.AddReport(new Reports
            {
                ReportId = reportDbId,
                IssuerGmail = request.IssuerEmail,
                ReportImpact = request.ReportImpact,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ResponseId = "",
            });

            return new ReportFormatRecord(reportResult);
        }
    }
}