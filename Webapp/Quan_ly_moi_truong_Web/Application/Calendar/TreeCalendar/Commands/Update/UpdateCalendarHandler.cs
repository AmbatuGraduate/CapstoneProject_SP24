using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Update
{
    public class UpdateCalendarHandler : IRequestHandler<UpdateCalendarCommand, ErrorOr<MyUpdatedEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;


        public UpdateCalendarHandler(ITreeCalendarService treeCalendarService,
            INotifyService notifyService, INotificationRepository notificationRepository)
        {
            _treeCalendarService = treeCalendarService;

            this.notifyService = notifyService;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<MyUpdatedEventResult>> Handle(UpdateCalendarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.UpdateEvent(request.accessToken, request.calendarId, request.myEvent, request.eventId);

            if(eventResult.Attendees.Count > 0)
            {
                // notification to all user about update
                var msg = "Lịch cắt tỉa cây mới đã được cập nhật";

                // Get all user in calendar
                var listUser = eventResult.Attendees;

                for (int i = 0; i < listUser.Count; i++)
                {
                    var notification = new Domain.Entities.Notification.Notifications
                    {
                        Id = Guid.NewGuid(),
                        Sender = "Hệ Thống",
                        Username = eventResult.Attendees[i].Email,
                        Message = msg,
                        MessageType = "Single",
                        NotificationDateTime = DateTime.Now,
                    };
                    await notificationRepository.CreateNotification(notification);
                    await notifyService.SendToUser(listUser[i].Email, msg);
                }

                // Send notification to admin about new calendar created
                var notifyAmin = new Domain.Entities.Notification.Notifications
                {
                    Id = Guid.NewGuid(),
                    Sender = "Hệ Thống",
                    Username = "ambatuadmin@vesinhdanang.xyz",
                    Message = "Vừa có lịch mới được tạo",
                    MessageType = "Single",
                    NotificationDateTime = DateTime.Now,
                };
                await notificationRepository.CreateNotification(notifyAmin);
                await notifyService.SendToUser("ambatuadmin@vesinhdanang.xyz", msg);

            }


            return new MyUpdatedEventResult(eventResult);
        }
    }
}