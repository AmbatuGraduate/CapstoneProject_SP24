using Domain.Entities.Cultivar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TreeType
{
    [DataContract]
    public class TreeTypes
    {
        [DataMember]
        public Guid TreeTypeId { get; set; }
        [DataMember]
        public string TreeTypeName { get; set; }

        public ICollection<Cultivars>? Cultivars { get; set; }

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
