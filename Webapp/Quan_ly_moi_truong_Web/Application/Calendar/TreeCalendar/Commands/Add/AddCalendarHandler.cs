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

            // notification to all attendees tree trim calendar
            var msg = "Bạn vừa có 1 lịch cắt tỉa cây";

            if(eventResult != null && eventResult.Attendees != null)
            {
                for (int i = 0; i < eventResult.Attendees.Count; i++)
                {
                    var notifyAttendees = new Domain.Entities.Notification.Notifications
                    {
                        Id = Guid.NewGuid(),
                        Sender = "Hệ Thống",
                        Username = eventResult.Attendees[i].Email,
                        Message = msg,
                        MessageType = "Single",
                        NotificationDateTime = DateTime.Now,
                    };
                    await notificationRepository.CreateNotification(notifyAttendees);
                    await notifyService.SendToUser(eventResult.Attendees[i].Email, msg);
                }
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

            return new MyAddedEventResult(eventResult);
        }
    }
}