using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class DuplicateCultivar
        {
            public static Error duplicateCultivar = Error.Conflict(
                 code: "duplicate.DuplicateCultivar", description: "Duplicate Cultivar");
        }
    }
}