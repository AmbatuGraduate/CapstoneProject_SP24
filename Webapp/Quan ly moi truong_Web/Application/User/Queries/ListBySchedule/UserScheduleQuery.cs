using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;


namespace Application.User.Queries.ListBySchedule
{
    public record UserScheduleQuery(Guid UserId) : IRequest<ErrorOr<List<ScheduleTreeTrimResult>>>;
}
