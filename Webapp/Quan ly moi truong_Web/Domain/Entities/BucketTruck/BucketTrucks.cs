using Domain.Entities.ScheduleTreeTrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BucketTruck
{
    [DataContract]
    public class BucketTrucks
    {
        [DataMember]
        public Guid BucketTruckId { get; set; }
        [DataMember]
        public string BucketTruckLicensePlates { get; set; }
        [DataMember]
        public float CraneArmLength { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [DataMember]
        public string CreateBy { get; set; }
        [DataMember]
        public DateTime UpdateDate { get; set; }
        [DataMember]
        public string UpdateBy { get; set; }

        public ICollection<ScheduleTreeTrims>? ScheduleTreeTrims { get; set; }
    }
}
