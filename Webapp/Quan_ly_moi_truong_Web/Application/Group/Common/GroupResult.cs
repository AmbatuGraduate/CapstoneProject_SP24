using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Common
{
    public class GroupResult
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AdminCreated { get; set; }
        public long DirectMembersCount { get; set; }
    }
}
