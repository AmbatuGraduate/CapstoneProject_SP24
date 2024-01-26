using Domain.Entities.GarbageTruck;
using System.Runtime.Serialization;

namespace Domain.Entities.GarbageTruckType
{
    [DataContract]
    public class GarbageTruckTypes
    {
        [DataMember]
        public Guid GarbageTruckTypeId { get; set; }

        [DataMember]
        public string GarbageTruckTypeName { get; set; }

        public ICollection<GarbageTrucks>? GarbageTrucks { get; set; }

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