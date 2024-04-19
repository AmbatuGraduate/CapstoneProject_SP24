using Application.Calendar;
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
        private readonly IUserRepository userRepository;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;

        public CreateReportCommandHandler(IReportService reportRepository, INotifyService notifyService,
                INotificationRepository notificationRepository,
                IUserRepository userRepository)
        {
            this.reportRepository = reportRepository;

            this.userRepository = userRepository;
            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<ReportFormatRecord>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var reportDbId = Guid.NewGuid().ToString();
            ReportFormat createReport = new()
            {
                Id = reportDbId,
                AccessToken = request.AccessToken,
                IssuerEmail = request.IssuerEmail,
                ReportSubject = request.ReportSubject,
                ReportBody = request.ReportBody,
                IssueLocation = request.IssueLocation,
                ReportImages = request.ReportImages,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ReportImpact = request.ReportImpact
            };

            // add to google
            var reportResult = await reportRepository.CreateReport(createReport);

            await reportRepository.AddReport(new Reports
            {
                ReportId = reportDbId,
                IssuerGmail = request.IssuerEmail,
                IssueLocation = request.IssueLocation,
                ReportImpact = request.ReportImpact,
                ExpectedResolutionDate = request.ExpectedResolutionDate,
                ResponseId = "",
            });

            var managers = userRepository.GetAll().Where(x => x.RoleId == Guid.Parse("abccde85-c7dc-4f78-9e4e-b1b3e7abee84")).ToList();
            //Notification report 
            var msg = "Bạn vừa nhận được báo cáo cần được xử lý";

            foreach(var user in managers)
            {
                var notification = new Domain.Entities.Notification.Notifications
                {
                    Id = Guid.NewGuid(),
                    Sender = reportResult.IssuerEmail,
                    Username = user.Email,
                    Message = msg,
                    MessageType = "Single",
                    NotificationDateTime = DateTime.Now,
                };
                await notificationRepository.CreateNotification(notification);
                await notifyService.SendToUser(user.Email, msg);
            }

            return new ReportFormatRecord(reportResult);
        }
    }
}