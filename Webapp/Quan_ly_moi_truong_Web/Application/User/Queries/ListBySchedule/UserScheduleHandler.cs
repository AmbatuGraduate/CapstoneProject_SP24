using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.ScheduleTreeTrim.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;


namespace Application.User.Queries.ListBySchedule
{
    public class UserScheduleHandler : IRequestHandler<UserScheduleQuery, ErrorOr<List<ScheduleTreeTrimResult>>>
    {
        private readonly IUserRepository userRepository;
        private readonly IScheduleTreeTrimRepository scheduleTreeTrimRepository;

        public UserScheduleHandler(IUserRepository userRepository, IScheduleTreeTrimRepository scheduleTreeTrimRepository)
        {
            this.userRepository = userRepository;
            this.scheduleTreeTrimRepository = scheduleTreeTrimRepository;
        }

        public async Task<ErrorOr<List<ScheduleTreeTrimResult>>> Handle(UserScheduleQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            //var user = userRepository.GetById(request.UserId);
            //if (user == null)
            //{
            //    return Errors.GetScheduleTreeTrimById.getScheduleFail;
            //}

            //var schedules = userRepository.GetSchedulesByUserId(request.UserId);
            return new List<ScheduleTreeTrimResult>(/*schedules.Select(s => new ScheduleTreeTrimResult(s)).ToList()*/);
        }
    }
}
