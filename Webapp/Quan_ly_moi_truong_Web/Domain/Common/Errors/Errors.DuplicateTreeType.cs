using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class DuplicateTreeType
        {
            public static Error duplicationTreeType = Error.Conflict(
                code: "duplicate.DuplicateTreeType", description: "Duplicate Tree Type");
        }
    }
}
