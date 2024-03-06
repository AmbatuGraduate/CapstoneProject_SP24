using Mapster;

namespace API.Mapping
{
    public class ScheduleTreeTrimMappingConfig : IRegister
    {
        // Update At: 03/02/2024 14:27
        // updated by: Dang Ngiuyen Khanh Vu
        // Changes:
        // - Comment lại nhưng mapping config cũ

        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<ScheduleTreeTrimResult, ListScheduleTreeTrimResponse>()
            //    .Map(dest => dest.BucketTruckId, src => src.scheduleTreeTrim.BucketTruckId)
            //    .Map(dest => dest.EstimatedPruningTime, src => src.scheduleTreeTrim.EstimatedPruningTime)
            //    .Map(dest => dest.ActualTrimmingTime, src => src.scheduleTreeTrim.ActualTrimmingTime);

            //config.NewConfig<Guid, GetByIdQuery>()
            //   .Map(dest => dest.ScheduleTreeTrimId, src => src);
        }
    }
}