<<<<<<< HEAD
﻿using System.Runtime.Serialization;
using Domain.Entities.ScheduleTreeTrim;
=======
﻿using Domain.Entities.ScheduleTreeTrim;
>>>>>>> vu/feature/get-information-of-cultivar
using Domain.Entities.Street;
using System.Runtime.Serialization;

namespace Domain.Entities.ScheduleTreeTrim_street_map
{
    public class ScheduleTreeTrim_street_maps
    {
        [DataMember]
        public Guid StreetId { get; set; }

        public virtual Streets? Street { get; set; }

        [DataMember]
        public Guid ScheduleTreeTrimId { get; set; }

        public virtual ScheduleTreeTrims? ScheduleTreeTrim { get; set; }

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