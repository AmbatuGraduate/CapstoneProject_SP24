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
        public static class GetCultivarById
        {
            public static Error getCultivarById = Error.NotFound(code: "get.GetCultivarById", description: "Cultivar Not Found");
        }
    }
}
