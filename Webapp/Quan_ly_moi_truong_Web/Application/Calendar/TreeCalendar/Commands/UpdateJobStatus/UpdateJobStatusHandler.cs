using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Calendar.TreeCalendar.Commands.UpdateJobStatus
{
    public class UpdateJobStatusHandler : IRequestHandler<UpdateJobStatusCommand, ErrorOr<MyUpdatedJobStatusResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly ITreeRepository _treeRepository;

        public UpdateJobStatusHandler(ITreeCalendarService treeCalendarService, ITreeRepository treeRepository)
        {
            _treeCalendarService = treeCalendarService;
            _treeRepository = treeRepository;
        }

        public async Task<ErrorOr<MyUpdatedJobStatusResult>> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            var eventResult = false;
            await Task.CompletedTask;
            var listTreesDb = _treeRepository.GetAllTrees();
            var listTreesNeedUpdated = await _treeCalendarService.UpdateJobStatus(request.accessToken, request.calendarId, request.jobWorkingStatus, request.eventId);
            if(!listTreesNeedUpdated.IsNullOrEmpty()) { 
                var listTrees = listTreesNeedUpdated.Split(',');
                foreach(var tree in listTrees)
                {
                    var treeNeedUpdated = listTreesDb.FirstOrDefault(t => t.TreeCode.Equals(tree.Trim(), StringComparison.OrdinalIgnoreCase));
                    if(treeNeedUpdated != null && treeNeedUpdated.CutTime.CompareTo(DateTime.Now) <= 0 && !treeNeedUpdated.isCut) 
                    { 
                        // update tree isCut status
                        treeNeedUpdated.isCut = true;
                        // update tree cutTime
                        treeNeedUpdated.CutTime = CalculateTreeNextCutTime(treeNeedUpdated.CutTime, treeNeedUpdated.IntervalCutTime);
                        _treeRepository.UpdateTree(treeNeedUpdated);
                        eventResult = true;
                    }
                }
            }
            return new MyUpdatedJobStatusResult(eventResult);
        }

        public DateTime CalculateTreeNextCutTime(DateTime currentCutTime, int intervalCutTime)
        {
            return currentCutTime.AddMonths(intervalCutTime);
        }
    }
}