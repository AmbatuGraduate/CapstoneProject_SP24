using Domain.Entities.ScheduleCleanSidewalk;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ListSidewalkCleanerTask
{
    [DataContract]
    public class ListSidewalkCleanerTasks
    {
        [DataMember]
        public Guid ListSidewalkCleanerTaskId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        public virtual Users? Users { get; set; }

        [DataMember]
        public Guid ScheduleCleanSidewalkId { get; set; }
        public virtual ScheduleCleanSidewalks? ScheduleCleanSidewalks { get; set; }

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
