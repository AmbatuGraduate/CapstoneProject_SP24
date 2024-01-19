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