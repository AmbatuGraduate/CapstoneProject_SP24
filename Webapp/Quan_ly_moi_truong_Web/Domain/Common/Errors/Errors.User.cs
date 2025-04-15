using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateUser = Error.Conflict(
                code: "user.DuplicateUser", description: "User is already exist");

            public static Error NotExist = Error.NotFound(
                code: "user.NotExist", description: "User is not found");
        }
    }
}