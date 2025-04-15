using Domain.Entities.Deparment;
using Domain.Entities.Role;
using Domain.Entities.Tree;
using Domain.Entities.UserRefreshToken;
using System.Runtime.Serialization;

namespace Domain.Entities.User
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserCode { get; set; } // Bỏ

        [DataMember]
        public string Email { get; set; } = null!;

        [DataMember]
        public Guid RoleId { get; set; }

        public virtual Roles? Role { get; set; }

        [DataMember]
        public string? DepartmentId { get; set; }

        public virtual Departments? Departments { get; set; }
        public ICollection<UserRefreshTokens>? UserRefreshTokens { get; set; }
        public ICollection<Trees>? trees { get; set; }
    }
}