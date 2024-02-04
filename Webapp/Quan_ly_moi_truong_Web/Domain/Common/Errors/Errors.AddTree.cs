using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class AddTree
        {
            public static Error AddTreeFail = Error.Failure(
                code: "add.AddTree", description: "Add Fail.");
        }
    }
}
