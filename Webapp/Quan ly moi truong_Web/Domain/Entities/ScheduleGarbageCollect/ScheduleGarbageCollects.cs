﻿using Domain.Entities.GarbageTruck;
using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ScheduleGarbageCollect
{
    [DataContract]
    public class ScheduleGarbageCollects
    {
        [DataMember]
        public Guid ScheduleGarbageCollectId { get; set; }
        [DataMember]
        public float GabageMass { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime TransitTime { get; set; }
        [DataMember]
        public DateTime WorkingMonth { get; set; }
        [DataMember]
        public Guid GarbageTruckId { get; set; }
        public virtual GarbageTrucks? GarbageTrucks { get; set; }

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

        public ICollection<ListGarbagemanTasks>? ListGarbagemanTasks { get; set; }
    }
}
