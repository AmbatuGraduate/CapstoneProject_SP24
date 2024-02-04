using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ScheduleCleanSidewalk;
using Domain.Entities.Street;

namespace Domain.Entities.ScheduleCleanSidewalk_street_map
{
    public class ScheduleCleanSidewalk_street_maps
    {
        [DataMember]
        public Guid StreetId { get; set; }
        public virtual Streets? Street {get; set;}
        [DataMember]
        public Guid ScheduleCleanSidewalksId { get; set; }
        public virtual ScheduleCleanSidewalks? ScheduleCleanSidewalk {get; set;}
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
