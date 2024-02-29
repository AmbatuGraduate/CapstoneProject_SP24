using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;
using Domain.Common.Errors;
using Application.Common.Interfaces.Persistence.Schedules;


namespace Application.ScheduleTreeTrim.Queries.GetById
{
    public class GetByIdHandler :
        IRequestHandler<GetByIdQuery, ErrorOr<ScheduleTreeTrimResult>>
    {

        private readonly IScheduleTreeTrimRepository scheduleTreeTrimRepository;

        // constructor
        public GetByIdHandler(IScheduleTreeTrimRepository scheduleTreeTrimRepository)
        {
            this.scheduleTreeTrimRepository = scheduleTreeTrimRepository;
        }

        // method to handle the request and return a result of type ErrorOr<ScheduleTreeTrimResult> 
        public async Task<ErrorOr<ScheduleTreeTrimResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var scheduleTreeTrim = scheduleTreeTrimRepository.GetScheduleTreeTrimById(request.ScheduleTreeTrimId);

            if (scheduleTreeTrim == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            return new ScheduleTreeTrimResult(scheduleTreeTrim);
        }
    }
}
