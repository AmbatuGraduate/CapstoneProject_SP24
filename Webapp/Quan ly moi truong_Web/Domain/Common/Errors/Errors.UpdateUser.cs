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
        public static class UpdateUser
        {
            public static Error UpdateUserFail = Error.Failure(
                code: "auth.UpdateUser", description: "Update Fail.");
        }
    }
}
