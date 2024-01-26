using Domain.Entities.BucketTruck;
using Domain.Entities.ListTreeTrimmerTask;
using Domain.Entities.ScheduleTreeTrim_street_map;
using System.Runtime.Serialization;


namespace Domain.Entities.ScheduleTreeTrim
{
    [DataContract]
    public class ScheduleTreeTrims
    {
        [DataMember]
        public Guid ScheduleTreeTrimId { get; set; }

        [DataMember]
        public Guid BucketTruckId { get; set; }

        public virtual BucketTrucks? BucketTrucks { get; set; }

        [DataMember]
        public DateTime EstimatedPruningTime { get; set; }

        [DataMember]
        public DateTime ActualTrimmingTime { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [DataMember]
        public string CreateBy { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string UpdateBy { get; set; }

        public ICollection<User_scheduleTreeTrim_maps>? User_scheduleTreeTrim_maps { get; set; }
        public ICollection<ScheduleTreeTrim_street_maps>? ScheduleTreeTrim_street_maps { get; set; }
    }
}