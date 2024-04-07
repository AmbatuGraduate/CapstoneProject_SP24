using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Report.Common;
using Domain.Entities.Report;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Create
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, ErrorOr<ReportFormatRecord>>
    {
        private readonly IReportService reportRepository;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;

        public CreateReportCommandHandler(IReportService reportRepository, INotifyService notifyService, INotificationRepository notificationRepository)
        {
            this.reportRepository = reportRepository;


            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
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
                ReportImage = request.ReportImage,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ReportImpact = request.ReportImpact
            };

            // add to google
            var reportResult = await reportRepository.CreateReport(createReport);

            // add to db

            await reportRepository.AddReport(new Reports
            {
                ReportId = reportDbId,
                IssuerGmail = request.IssuerEmail,
                ReportImpact = request.ReportImpact,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ResponseId = "",
            });

            //Notification report
            var msg = "Bạn vừa nhận được báo cáo";

            var notification = new Domain.Entities.Notification.Notifications
            {
                Id = Guid.NewGuid(),
                Username = "ambatuadmin@vesinhdanang.xyz",
                Message = msg,
                MessageType = "Single",
                NotificationDateTime = DateTime.Now,
            };
            await notificationRepository.CreateNotification(notification);
            await notifyService.SendToUser("ambatuadmin@vesinhdanang.xyz", msg);
            return new ReportFormatRecord(reportResult);
        }
    }
}