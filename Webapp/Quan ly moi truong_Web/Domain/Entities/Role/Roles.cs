using Domain.Entities.User;
using System.Runtime.Serialization;

namespace Domain.Entities.Role
{
    [DataContract]
    public class Roles
    {
        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

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