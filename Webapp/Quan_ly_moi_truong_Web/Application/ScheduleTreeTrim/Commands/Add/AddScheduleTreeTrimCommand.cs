using Application.ScheduleTreeTrim.Common;
using ErrorOr;
using MediatR;

namespace Application.ScheduleTreeTrim.Commands.Add
{
    public record AddScheduleTreeTrimCommand(
              Guid BucketTruckId,
              DateTime EstimatedPruningTime,
              DateTime ActualTrimmingTime,
              string CreateBy,
              DateTime UpdateDate,
              string UpdateBy
     ) : IRequest<ErrorOr<ScheduleTreeTrimResult>>;
}
