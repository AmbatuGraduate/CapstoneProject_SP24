using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
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

        public AutoAddTreeCalendarHandler(ITreeCalendarService treeCalendarService, ITreeRepository treeRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _treeCalendarService = treeCalendarService;
            _treeRepository = treeRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<List<MyAddedEventResult>>> Handle(AutoAddTreeCalendarCommand request, CancellationToken cancellationToken)
        {
            List<MyAddedEventResult> eventResults = new List<MyAddedEventResult>();

            var accessToken = _jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);

            var getAllTree = _treeRepository.GetAllTrees().Where(tree => !tree.isCut);

            var treeByAddress = getAllTree
                .GroupBy(tree => Regex.Replace(tree.TreeLocation, @"^\d+\s+", string.Empty).Split(",")[0])
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(tree => Regex.Replace(tree.TreeLocation, @"^\d+\s+", string.Empty).Split(",")[0].ToLower() == group.Key.Split(",")[0].ToLower())
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
                    Description = "Đến thởi điểm cắt tỉa các cây đã đến hạn tại đường " + group.Key,
                    location = group.Key,
                    TreeId = treeFormat,
                    Start = new EventDateTime
                    {
                        DateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
                    },
                    End = new EventDateTime
                    {
                        DateTime = DateTime.Now.AddHours(3).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
                    },
                    Attendees = new List<User>()
                };

                var result = await _treeCalendarService.AddEvent(accessToken, request.calendarId, addedEvent);
                eventResults.Add(new MyAddedEventResult(result));
            }
            return eventResults;
        }
    }
}