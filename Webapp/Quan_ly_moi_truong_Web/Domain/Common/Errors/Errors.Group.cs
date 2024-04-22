using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Group
        {
            public static Error notFoundGroup = Error.NotFound(code: "get.notFoundGroup", description: "Not found group");
        }
    }
}
