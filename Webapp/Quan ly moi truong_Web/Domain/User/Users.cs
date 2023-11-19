using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users : IdentityUser<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
