

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
        private readonly IUserRepository userRepository;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;

        // constructor
        public DeleteReportHandler(IReportService reportService,
            INotifyService notifyService, INotificationRepository notificationRepository,
            IUserRepository userRepository)
        {
            _reportService = reportService;

            this.userRepository = userRepository;
            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var report = await _reportService.DeleteReport(request.ReportID);

            var managers = userRepository.GetAll().Where(x => x.RoleId == Guid.Parse("abccde85-c7dc-4f78-9e4e-b1b3e7abee84")).ToList();
            //Notification report
            var usserEmail = report.IssuerGmail;
            var msg = "Báo cáo của bạn đã được xóa";

            foreach(var manager in managers) 
            {
                var notification = new Domain.Entities.Notification.Notifications
                {
                    Id = Guid.NewGuid(),
                    Sender = manager.Email,
                    Username = usserEmail,
                    Message = msg,
                    MessageType = "Single",
                    NotificationDateTime = DateTime.Now,
                };
                await notificationRepository.CreateNotification(notification);
                await notifyService.SendToUser(usserEmail, msg);
            }

            return Unit.Value;
        }
    }
}
