using Domain.Entities.ListSidewalkCleanerTask;
using Domain.Entities.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ScheduleCleanSidewalk
{
    [DataContract]
    public class ScheduleCleanSidewalks
    {
        [DataMember]
        public Guid ScheduleCleanSidewalksId { get; set; }
        [DataMember]
        public Guid StreetId { get; set; }
        public virtual Streets? Streets { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime WorkingMonth { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [DataMember]
        public string CreateBy { get; set; }
        [DataMember]
        public DateTime UpdateDate { get; set; }
        [DataMember]
        public string UpdateBy { get; set; }

        public ICollection<ListSidewalkCleanerTasks>? ListSidewalkCleanerTasks { get; set; }

    }
}
