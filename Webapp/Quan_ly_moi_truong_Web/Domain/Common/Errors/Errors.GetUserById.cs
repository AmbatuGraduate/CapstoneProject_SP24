using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetUserById
        {
            public static Error getUserFail = Error.NotFound(
                               code: "get.GetUserById", description: "Not found user");
        }
    }
}
