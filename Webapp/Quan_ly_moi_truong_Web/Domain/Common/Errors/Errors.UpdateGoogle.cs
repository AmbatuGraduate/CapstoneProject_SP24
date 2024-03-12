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
        public static class UpdateGoogle
        {
            public static Error UpdateGoogleUserFail = Error.Failure(
                           code: "auth.UpdateGoogleUser", description: "Failed to update user.");

        }
    }
}
