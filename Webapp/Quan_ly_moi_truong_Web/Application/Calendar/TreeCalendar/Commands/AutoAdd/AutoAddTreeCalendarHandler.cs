using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Report.Common;
using ErrorOr;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public class AutoAddTreeCalendarHandler
        : IRequestHandler<AutoAddTreeCalendarCommand, ErrorOr<List<MyAddedEventResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly ITreeRepository _treeRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        //For notification
        private readonly INotifyService notifyService;
        private readonly INotificationRepository notificationRepository;
        private readonly IUserRepository userRepository;

        public AutoAddTreeCalendarHandler(ITreeCalendarService treeCalendarService, ITreeRepository treeRepository, IJwtTokenGenerator jwtTokenGenerator,
            INotifyService notifyService, INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _treeCalendarService = treeCalendarService;
            _treeRepository = treeRepository;
            _jwtTokenGenerator = jwtTokenGenerator;

            this.notifyService = notifyService;
            this.userRepository = userRepository;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<List<MyAddedEventResult>>> Handle(AutoAddTreeCalendarCommand request, CancellationToken cancellationToken)
        {
            List<MyAddedEventResult> eventResults = new List<MyAddedEventResult>();

            var getAllTree = _treeRepository.GetAllTrees().Where(tree => !tree.isCut);

            var treeByAddress = getAllTree
                .GroupBy(tree => tree.TreeLocation.Substring(tree.TreeLocation.IndexOf(",")))
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(tree => group.Key.Substring(group.Key.IndexOf(",")).ToLower().Contains(tree.TreeLocation.Split(",", StringSplitOptions.TrimEntries)[1].ToLower()))
                .ToList()).ToList();

            foreach (var group in treeByAddress)
            {
                System.Diagnostics.Debug.WriteLine("Group: " + group.Key + " - " + group.Value.Count);
                //list tree seprate with comma
                string treeFormat = string.Empty;
                foreach (var item in group.Value)
                {
                    treeFormat += item.TreeCode + ",";
                }

                var addedEvent = new MyAddedEvent
                {
                    Summary = "Lịch cắt tỉa cây tại " + group.Key,
                    Description = "Đến thời điểm cắt tỉa các cây đã đến hạn tại đường " + group.Key,
                    location = group.Key + ",Đà Nẵng",
                    TreeId = treeFormat,
                    DepartmentEmail = "cayxanh@vesinhdanang.xyz",
                    Start = new EventDateTime
                    {
                        DateTime = DateTime.Now.ToLocalTime().ToString()
                    },
                    End = new EventDateTime
                    {
                        DateTime = DateTime.Now.AddHours(3).ToLocalTime().ToString()
                    },
                    Attendees = new List<User>()
                };

                var result = await _treeCalendarService.AutoAddEvent(request.Service, request.CalendarId, addedEvent);
                eventResults.Add(new MyAddedEventResult(result));
            }

            // notification to all manager tree trim calendar
            var treeTrimDepartmentId = "01egqt2p26jkcil"; // Id department of tree
            var roleId = "abccde85-c7dc-4f78-9e4e-b1b3e7abee84"; // Id role
            var msg = eventResults.ToList().Count + " Lịch cắt tỉa cây mới đã được tạo";

            // Get all user have department id is tree
            var listUser = userRepository.GetAll().Where(x => x.DepartmentId == treeTrimDepartmentId && x.RoleId == Guid.Parse(roleId)).ToList();

            for (int i = 0; i < listUser.Count; i++)
            {
                var notification = new Domain.Entities.Notification.Notifications
                {
                    Id = Guid.NewGuid(),
                    Sender = "Hệ Thống",
                    Username = listUser[i].Email,
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

            return eventResults;
        }
    }
}