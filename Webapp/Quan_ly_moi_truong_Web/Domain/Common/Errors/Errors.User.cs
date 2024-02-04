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
        public static class User
        {
            public static Error DuplicateUser = Error.Conflict(
                code: "user.DuplicateUser", description: "User is already exist");
        }
    }
}
