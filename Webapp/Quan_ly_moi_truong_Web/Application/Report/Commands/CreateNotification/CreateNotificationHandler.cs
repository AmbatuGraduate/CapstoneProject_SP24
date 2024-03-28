using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Report.Commands.Create;
using Application.Report.Common;
using Domain.Entities.Report;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Report.Commands.CreateNotification
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, ErrorOr<ReportFormatRecord>>
    {
        private readonly IReportService reportRepository;
        private readonly IUserRepository userRepository;
        private readonly ITreeCalendarService treeCalendarService;
        private readonly IReportService reportService;

        public CreateNotificationHandler(IReportService reportRepository, 
            IUserRepository userRepository, ITreeCalendarService treeCalendarService, IReportService reportService)
        {
            this.reportRepository = reportRepository;
            this.userRepository = userRepository;
            this.treeCalendarService = treeCalendarService;
            this.reportService = reportService;
        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportDbId = Guid.NewGuid().ToString();

            //Get list of mail manager base on department
            var addressMails = string.Empty;
            var userList = userRepository.GetAll();

            // Create a body content for mail


            // Create Report
            ReportFormat createReport = new ReportFormat
            {
                Id = reportDbId,
                AccessToken = request.AccessToken,
                IssuerEmail = request.IssuerEmail,
                ReportSubject = null,
                ReportBody = null,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ReportImpact = request.ReportImpact
            };

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
