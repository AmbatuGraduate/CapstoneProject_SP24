using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;


namespace Application.ScheduleTreeTrim.Queries.GetById
{
    public record GetByIdQuery(Guid ScheduleTreeTrimId) : IRequest<ErrorOr<ScheduleTreeTrimResult>>;
}
