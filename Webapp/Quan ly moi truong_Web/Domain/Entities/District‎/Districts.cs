using Domain.Entities.Ward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.District
{
    [DataContract]
    public class Districts
    {
        [DataMember]
        public Guid DistrictId { get; set; }
        [DataMember]
        public string DistrictName { get; set; } = string.Empty;
        public ICollection<Wards>? Wards { get; set; }

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
