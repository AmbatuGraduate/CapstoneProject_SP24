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


            // notification to all user about update
            var msg = "Lịch cắt tỉa cây mới đã được cập nhật";

            // Get all user in calendar
            var listUser = eventResult.Attendees;

            for (int i = 0; i < listUser.Count; i++)
            {
                var notification = new Domain.Entities.Notification.Notifications
                {
                    Id = Guid.NewGuid(),
                    Username = listUser[i].Email,
                    Message = msg,
                    MessageType = "Single",
                    NotificationDateTime = DateTime.Now,
                };
                notificationRepository.CreateNotification(notification);
                await notifyService.SendToUser(listUser[i].Email, msg);
            }

            return new MyUpdatedEventResult(eventResult);
        }
    }
}