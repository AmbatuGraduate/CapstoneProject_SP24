

using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Report.Common;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Delete
{
    public class DeleteReportHandler : IRequestHandler<DeleteReportCommand, ErrorOr<Unit>>
    {
        private readonly IReportService _reportService;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;

        // constructor
        public DeleteReportHandler(IReportService reportService,
            INotifyService notifyService, INotificationRepository notificationRepository)
        {
            _reportService = reportService;

            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var report = await _reportService.DeleteReport(request.ReportID);

            //Notification report
            var usserEmail = report.IssuerGmail;
            var msg = "Báo cáo của bạn đã được xóa";

            var notification = new Domain.Entities.Notification.Notifications
            {
                Id = Guid.NewGuid(),
                Sender = "hr@vesinhdanang.xyz",
                Username = usserEmail,
                Message = msg,
                MessageType = "Single",
                NotificationDateTime = DateTime.Now,
            };
            await notificationRepository.CreateNotification(notification);
            await notifyService.SendToUser(usserEmail, msg);

            return Unit.Value;
        }
    }
}
