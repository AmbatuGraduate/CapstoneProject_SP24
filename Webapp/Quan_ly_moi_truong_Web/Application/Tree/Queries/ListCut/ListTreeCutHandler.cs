using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Tree.Common;
using Domain.Entities.Tree;
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Tree.Queries.ListCut
{
    public class ListTreeCutHandler :
        IRequestHandler<ListTreeCutQuery, ErrorOr<List<TreeResult>>>
    {

        private readonly ITreeRepository treeRepository;
        private readonly ITreeTypeRepository treeTypeRepository;

        private readonly ITreeCalendarService _treeCalendarService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public ListTreeCutHandler(ITreeRepository treeRepository, ITreeTypeRepository treeTypeRepository,
            ITreeCalendarService treeCalendarService, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.treeRepository = treeRepository;
            this.treeTypeRepository = treeTypeRepository;

            _jwtTokenGenerator = jwtTokenGenerator;
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<TreeResult>>> Handle(ListTreeCutQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var accessToken = _jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);
            var calendarId = _treeCalendarService.GetCalendarIdByCalendarType(CalendarTypeEnum.CayXanh);
            var list = await _treeCalendarService.GetEvents(accessToken, calendarId);

            var results = new List<TreeResult>();

            // Group all tree that have same address
            var treeByAddresses = treeRepository.GetAllTrees()
                .Where(x => x.isCut == false)
                .GroupBy(tree => Regex.Replace(request.Address, @"^\d+\s+", string.Empty).Split(",")[0])
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(tree => Regex.Replace(tree.TreeLocation.ToLower(), @"^\d+\s+", string.Empty).Split(",")[0].ToLower() == group.Key.Split(",")[0].ToLower()));

            // List all event that has be in progress or note start and in same address
            var listCalendar = list.Where(x => ((x.ExtendedProperties.PrivateProperties["JobWorkingStatus"] == _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart))
                                                    || (x.ExtendedProperties.PrivateProperties["JobWorkingStatus"] == _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.InProgress)))
                                                    && (Regex.Replace(x.Location.ToLower(), @"^\d+\s+", string.Empty).Split(",")[0].ToLower() == Regex.Replace(request.Address, @"^\d+\s+", string.Empty).Split(",")[0].ToLower())).ToList();

            // Get list of tree that have been in cut but not finish
            StringBuilder treeInCalendar = new StringBuilder();
            foreach (var item in listCalendar)
            {
                treeInCalendar.Append(item.ExtendedProperties.PrivateProperties["Tree"]);
            }

            foreach (var trees in treeByAddresses)
            {
                foreach (var tree in trees.Value)
                {
                    if (!treeInCalendar.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(tree.TreeCode))
                    {
                        var treeType = treeTypeRepository.GetTreeTypeById(tree.TreeTypeId).TreeTypeName;
                        var result = new TreeResult(tree.TreeCode, tree.TreeLocation, treeType, tree.BodyDiameter, tree.LeafLength, tree.CutTime, tree.isCut);
                        results.Add(result);
                    }
                }
            }
            return results;
        }
    }
}
