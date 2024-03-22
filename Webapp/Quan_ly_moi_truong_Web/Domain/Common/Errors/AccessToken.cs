﻿using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class AccessToken
        {
            public static Error InvalidAccessToken = Error.Validation(
                           code: "auth.DuplicateUser", description: "Invalid Access token.");

        }
    }
}
