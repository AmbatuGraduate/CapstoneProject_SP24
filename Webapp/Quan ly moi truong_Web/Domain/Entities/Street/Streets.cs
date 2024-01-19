using Domain.Entities.GarbageDump;
using Domain.Entities.ScheduleCleanSidewalk_street_map;
using Domain.Entities.ScheduleGarbageCollect_street_map;
using Domain.Entities.ScheduleTreeTrim_street_map;
using Domain.Entities.StreetType;
using Domain.Entities.Tree;
using Domain.Entities.Ward;
<<<<<<< HEAD

using System.Runtime.Serialization;

=======
using System.Runtime.Serialization;
>>>>>>> vu/feature/get-information-of-cultivar

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

        public ICollection<ScheduleCleanSidewalk_street_maps>? ScheduleCleanSidewalk_street_maps { get; set; }
        public ICollection<ScheduleGarbageCollect_street_maps>? ScheduleGarbageCollect_street_maps { get; set; }
        public ICollection<ScheduleTreeTrim_street_maps>? ScheduleTreeTrim_street_maps { get; set; }
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