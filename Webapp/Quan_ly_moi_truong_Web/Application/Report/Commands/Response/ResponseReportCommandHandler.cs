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

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;

        public ResponseReportCommandHandler(IReportService reportRepository, INotifyService notifyService, INotificationRepository notificationRepository)
        {
            this.reportRepository = reportRepository;

            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(ReponseReportCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportResult = await reportRepository.ReponseReport(request.AccessToken, request.ReportID, request.Response, request.Status);


            //Notification report
            var usserEmail = reportResult.IssuerEmail;
            var msg = "Báo cáo của bạn đã được phản hồi";

            var notification = new Domain.Entities.Notification.Notifications
            {
                Id = Guid.NewGuid(),
                Sender = "ambatuadmin@vesinhdanang.xyz",
                Username = usserEmail,
                Message = msg,
                MessageType = "Single",
                NotificationDateTime = DateTime.Now,
            };
            await notificationRepository.CreateNotification(notification);
            await notifyService.SendToUser(usserEmail, msg);

            return new ReportFormatRecord(reportResult);
        }
    }
}