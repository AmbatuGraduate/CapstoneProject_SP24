using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Deparment
{
    [DataContract]
    public class Departments
    {
        [DataMember]
        public Guid DepartmentId { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }

        public ICollection<Users>? Users { get; set; }

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
