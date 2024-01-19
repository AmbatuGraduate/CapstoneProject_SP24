using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;

namespace Application.ScheduleTreeTrim.Queries.List
{
    public record ListScheduleTreeTrimQuery() : IRequest<ErrorOr<List<ScheduleTreeTrimResult>>>;
}
