using Domain.Entities.User;
using System.Runtime.Serialization;

namespace Domain.Entities.Deparment
{
    [DataContract]
    public class Departments
    {
        [DataMember]
        public string DepartmentId { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string DepartmentEmail { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool AdminCreated { get; set; }

        [DataMember]
        public long DirectMembersCount { get; set; }

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