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

    }
}