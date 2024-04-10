using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Add
{
    public class AddCalendarHandler : IRequestHandler<AddCalendarCommand, ErrorOr<MyAddedEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;
        private readonly IUserRepository userRepository;

        public AddCalendarHandler(ITreeCalendarService treeCalendarService,
            INotifyService notifyService, INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _treeCalendarService = treeCalendarService;

            this.notifyService = notifyService;
            this.userRepository = userRepository;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<MyAddedEventResult>> Handle(AddCalendarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.AddEvent(request.accessToken, request.calendarId, request.myEvent);

            // notification to all manager tree trim calendar
            var msg = "Bạn vừa có 1 lịch cắt tỉa cây";

            if(eventResult != null && eventResult.Attendees != null)
            {
                for (int i = 0; i < eventResult.Attendees.Count; i++)
                {
                    var notification = new Domain.Entities.Notification.Notifications
                    {
                        Id = Guid.NewGuid(),
                        Username = eventResult.Attendees[i].Email,
                        Message = msg,
                        MessageType = "Single",
                        NotificationDateTime = DateTime.Now,
                    };
                    await notificationRepository.CreateNotification(notification);
                    await notifyService.SendToUser(eventResult.Attendees[i].Email, msg);
                }
            }

            return new MyAddedEventResult(eventResult);
        }
    }
}