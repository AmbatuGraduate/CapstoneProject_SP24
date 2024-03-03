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
        public static class GetStreetById
        {
            public static Error getStreetFail = Error.NotFound(
                code: "get.GetStreetById", description: "Street not found");
        }
    }
}
