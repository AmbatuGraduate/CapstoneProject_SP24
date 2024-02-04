namespace Contract.ScheduleTreeTrim
{
    public record ListScheduleTreeTrimResponse
    (
        Guid BucketTruckId,
        DateTime EstimatedPruningTime,
        DateTime ActualTrimmingTime
    );
}
