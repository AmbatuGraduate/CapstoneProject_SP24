using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Domain.Common.Errors;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;


namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public class AutoAddTreeCalendarHandler
        : IRequestHandler<AutoAddTreeCalendarCommand, ErrorOr<List<MyAddedEventResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly ITreeRepository _treeRepository;

        public AutoAddTreeCalendarHandler(ITreeCalendarService treeCalendarService, ITreeRepository treeRepository)
        {
            _treeCalendarService = treeCalendarService;
            _treeRepository = treeRepository;
        }

        public async Task<ErrorOr<List<MyAddedEventResult>>> Handle(AutoAddTreeCalendarCommand request, CancellationToken cancellationToken)
        {

            //var accessToken = jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);


            List<MyAddedEventResult> eventResults = new List<MyAddedEventResult>();

            var getAllTree = _treeRepository.GetAllTrees();
            var cutTree = new List<Trees>();
            // check and auto update status isCut of tree
            foreach (var tree in getAllTree)
            {
                if (!tree.isCut)
                {
                    cutTree.Add(tree);
                }
            }


            var treeByAddress = cutTree
                .GroupBy(tree => Regex.Replace(tree.TreeLocation, @"^\d+\s+", string.Empty))
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(tree => Regex.Replace(tree.TreeLocation, @"^\d+\s+", string.Empty).ToLower() == group.Key.ToLower())
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

                var result = await _treeCalendarService.AddEvent(request.accessToken, request.calendarId, addedEvent);
                eventResults.Add(new MyAddedEventResult(result));

            }
            return eventResults;
        }
    }
}
