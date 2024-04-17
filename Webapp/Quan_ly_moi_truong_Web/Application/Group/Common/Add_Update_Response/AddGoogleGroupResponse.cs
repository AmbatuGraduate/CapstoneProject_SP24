using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Common.Add_Update_Response
{
    public class AddGoogleGroupResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owners { get; set; }  // nv1, nv2
        public string Members { get; set; } // nv1, nv2
        public bool AdminCreated { get; set; }
    }

    public class UpdateGoogleGroupResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owners { get; set; }  // nv1, nv2
        public string Members { get; set; } // nv1, nv2
        public bool AdminCreated { get; set; }
    }
}
