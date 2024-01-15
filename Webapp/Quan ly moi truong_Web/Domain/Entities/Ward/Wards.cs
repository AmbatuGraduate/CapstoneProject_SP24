
using Domain.Entities.District;
using Domain.Entities.GarbageDump;
using Domain.Entities.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Ward
{
    [DataContract]
    public class Wards
    {
        [DataMember]
        public Guid WardId { get; set; }
        [DataMember]
        public string WardName { get; set; }
        [DataMember]
        public Guid DistrictId { get; set; }
        public virtual Districts? Districts { get; set; }
        public ICollection<Streets>? Streets { get; set; }
        public ICollection<GarbageDumps>? GarbageDumps { get; set; }
    }
}
