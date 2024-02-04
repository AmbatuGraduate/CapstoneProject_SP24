using Domain.Entities.GarbageTruck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
