using Domain.Entities.GarbageDump;
using Domain.Entities.ScheduleCleanSidewalk;
using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.StreetType;
using Domain.Entities.Tree;
using Domain.Entities.Ward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Street
{
    [DataContract]
    public class Streets
    {
        [DataMember]
        public Guid StreetId { get; set; }
        [DataMember]
        public string StreetName { get; set; }
        [DataMember]
        public float StreetLength { get; set; }
        [DataMember]
        public int NumberOfHouses { get; set; }
        [DataMember]
        public Guid StreetTypeId { get; set; }
        public virtual StreetTypes? StreetType { get; set; }
        [DataMember]
        public Guid WardId { get; set; }
        public virtual Wards? Wards { get; set; }

        public ICollection<ScheduleTreeTrims>? ScheduleTreeTrims { get; set; }
        public ICollection<ScheduleGarbageCollects>? ScheduleGarbageCollects { get; set; }
        public ICollection<ScheduleCleanSidewalks>? ScheduleCleanSidewalks { get; set; }
        public ICollection<GarbageDumps>? GarbageDumps { get; set; }
        public ICollection<Trees>? Trees { get; set; }

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
