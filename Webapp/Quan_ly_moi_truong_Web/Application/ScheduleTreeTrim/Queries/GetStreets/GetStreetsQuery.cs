using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.ScheduleTreeTrim.Queries.GetStreets
{
    public record GetStreetsQuery(Guid ScheduleId) : IRequest<ErrorOr<List<StreetResult>>>;
}
