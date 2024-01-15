using Domain.Entities.GarbageTruck;
using Domain.Entities.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GarbageDump
{
    [DataContract]
    public class GarbageDumps
    {
        [DataMember]
        public Guid GarbageDumpId { get; set; }
        [DataMember]
        public string GarbageDumpName { get; set; }
        public ICollection<GarbageTrucks>? GarbageTrucks { get; set; }
        [DataMember]
        public Guid StreetId { get; set; }
        public virtual Streets? Streets { get; set; }

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
