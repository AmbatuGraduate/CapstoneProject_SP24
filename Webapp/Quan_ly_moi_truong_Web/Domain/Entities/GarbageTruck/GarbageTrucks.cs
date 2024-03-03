using Domain.Entities.GarbageDump;
using Domain.Entities.GarbageTruckType;
using Domain.Entities.ScheduleGarbageCollect;
using System.Runtime.Serialization;

namespace Domain.Entities.GarbageTruck
{
    [DataContract]
    public class GarbageTrucks
    {
        [DataMember]
        public Guid GarbageTruckId { get; set; }

        [DataMember]
        public string GarbageTruckLicensePlates { get; set; }

        [DataMember]
        public float GarbageTruckWeight { get; set; }

        [DataMember]
        public Guid GarbageTruckTypeId { get; set; }

        public virtual GarbageTruckTypes? GarbageTruckTypes { get; set; }

        [DataMember]
        public Guid GarbageDumpId { get; set; }

        public virtual GarbageDumps? GarbageDumps { get; set; }
        public ICollection<ScheduleGarbageCollects>? ScheduleGarbageCollects { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [DataMember]
        public string CreateBy { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string UpdateBy { get; set; }
    }
}