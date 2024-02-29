using ErrorOr;

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