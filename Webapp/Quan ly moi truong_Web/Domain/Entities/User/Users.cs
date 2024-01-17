using Domain.Entities.Deparment;
using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.ListSidewalkCleanerTask;
using Domain.Entities.ListTreeTrimmerTask;
using Domain.Entities.Report;
using Domain.Entities.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    [DataContract]
    public class Users : IdentityUser<Guid>
    {
        [DataMember]
        public override Guid Id { get; set; } = Guid.NewGuid();
        [DataMember]
        public string UserCode { get; set; }
        [DataMember]
        public string Name { get; set; } = null!;
        [DataMember]
        public string Address { get; set; } = null!;
        [DataMember]
        public override string PhoneNumber { get; set; } = null!;
        [DataMember]
        public string Password { get; set; } = null!;
        [DataMember]
        public Guid RoleId { get; set; }
        public virtual Roles? Role { get; set; }
        [DataMember]
        public Guid DepartmentId { get; set; }
        public virtual Departments? Departments { get; set; }
        [DataMember]
        public string Image { get; set; } = null!;

        public ICollection<Reports>? Reports { get; set; }
        public ICollection<User_scheduleGarbageCollect_maps>? User_scheduleGarbageCollect_maps { get; set; }
        public ICollection<User_scheduleTreeTrim_maps>? User_scheduleTreeTrim_maps { get; set; }
        public ICollection<User_scheduleCleanSidewalk_maps>? User_scheduleCleanSidewalk_maps { get; set; }



    }
}
