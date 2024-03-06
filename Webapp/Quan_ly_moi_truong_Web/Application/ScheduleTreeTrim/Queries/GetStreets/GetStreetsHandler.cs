using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Street.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.ScheduleTreeTrim.Queries.GetStreets
{
    public class GetStreetsHandler : IRequestHandler<GetStreetsQuery, ErrorOr<List<StreetResult>>>
    {
        private readonly IScheduleTreeTrimRepository scheduleTreeTrimRepository;
        private readonly IStreetRepository streetRepository;

        public GetStreetsHandler(IScheduleTreeTrimRepository scheduleTreeTrimRepository, IStreetRepository streetRepository)
        {
            this.scheduleTreeTrimRepository = scheduleTreeTrimRepository;
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<List<StreetResult>>> Handle(GetStreetsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //var schedule = scheduleTreeTrimRepository.GetScheduleTreeTrimById(request.ScheduleId);
            //if (schedule == null)
            //{
            //    return Errors.GetScheduleTreeTrimById.getScheduleFail;
            //}

            //var streets = scheduleTreeTrimRepository.GetStreetsOfSchedule(request.ScheduleId);
            return new List<StreetResult>(/*streets.Select(s => new StreetResult(s)).ToList()*/);
        }
    }
}
