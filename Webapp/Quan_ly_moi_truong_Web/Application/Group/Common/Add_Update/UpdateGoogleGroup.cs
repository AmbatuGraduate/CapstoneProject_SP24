using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Common.Add_Update
{
    public class UpdateGoogleGroup
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Owners { get; set; }
        public List<string> Members { get; set; }
        public bool AdminCreated { get; set; }
    }
}
