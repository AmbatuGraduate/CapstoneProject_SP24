using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ListGarbagemanTask
{
    [DataContract]
    public class ListGarbagemanTasks
    {
        [DataMember]
        public Guid ListGarbagemanTaskId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        public virtual Users? Users { get; set; }

        [DataMember]
        public Guid ScheduleGarbageCollectId { get; set; }
        public virtual ScheduleGarbageCollects? ScheduleGarbageCollects { get; set; }

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
