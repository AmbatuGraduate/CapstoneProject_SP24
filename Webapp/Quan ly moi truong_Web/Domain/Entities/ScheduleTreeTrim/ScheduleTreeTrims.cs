using Domain.Entities.BucketTruck;
using Domain.Entities.ListTreeTrimmerTask;
using Domain.Entities.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ScheduleTreeTrim
{
    [DataContract]
    public class ScheduleTreeTrims
    {
        [DataMember]
        public Guid ScheduleTreeTrimId { get; set; }
        [DataMember]
        public Guid StreetId { get; set; }
        public virtual Streets? Streets { get; set; }
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

        public ICollection<ListTreeTrimmerTasks>? ListTreeTrimmerTasks { get; set; }

    }
}
