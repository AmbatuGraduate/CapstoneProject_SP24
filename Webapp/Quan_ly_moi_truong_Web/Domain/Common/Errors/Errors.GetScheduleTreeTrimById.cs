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
        public static class GetScheduleTreeTrimById
        {
            public static Error getScheduleFail = Error.NotFound(
                               code: "get.GetScheduleTreeTrimById", description: "Schedule not found");
        }
    }
}
