using Application.Common.Interfaces.Persistence.Schedules;
using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;

namespace Application.ScheduleTreeTrim.Queries.List
{
    public class ListScheduleTreeTrimHandler : IRequestHandler<ListScheduleTreeTrimQuery, ErrorOr<List<ScheduleTreeTrimResult>>>
    {
        private readonly IScheduleTreeTrimRepository scheduleTreeTrimRepository;

        public ListScheduleTreeTrimHandler(IScheduleTreeTrimRepository scheduleTreeTrimRepository)
        {
            this.scheduleTreeTrimRepository = scheduleTreeTrimRepository;
        }

        public async Task<ErrorOr<List<ScheduleTreeTrimResult>>> Handle(ListScheduleTreeTrimQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<ScheduleTreeTrimResult> scheduleTreeTrimResults = new List<ScheduleTreeTrimResult>();
            //var scheduleTreeTrims = scheduleTreeTrimRepository.GetAllScheduleTreeTrims();

            //foreach (var scheduleTreeTrim in scheduleTreeTrims)
            //{
            //    scheduleTreeTrimResults.Add(new ScheduleTreeTrimResult(scheduleTreeTrim));
            //}

            return scheduleTreeTrimResults;
        }
    } 
}
