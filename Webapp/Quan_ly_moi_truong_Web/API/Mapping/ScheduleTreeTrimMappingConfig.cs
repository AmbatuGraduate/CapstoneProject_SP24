using Application.ScheduleTreeTrim.Common;
using Application.ScheduleTreeTrim.Queries.GetById;
using Contract.ScheduleTreeTrim;
using Mapster;

namespace API.Mapping
{
    public class ScheduleTreeTrimMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ScheduleTreeTrimResult, ListScheduleTreeTrimResponse>()
                .Map(dest => dest.BucketTruckId, src => src.scheduleTreeTrim.BucketTruckId)
                .Map(dest => dest.EstimatedPruningTime, src => src.scheduleTreeTrim.EstimatedPruningTime)
                .Map(dest => dest.ActualTrimmingTime, src => src.scheduleTreeTrim.ActualTrimmingTime);

            config.NewConfig<Guid, GetByIdQuery>()
               .Map(dest => dest.ScheduleTreeTrimId, src => src);
        }
    }
}
