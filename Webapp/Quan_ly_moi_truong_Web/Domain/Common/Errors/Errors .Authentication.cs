using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials = Error.Validation(
                           code: "auth.DuplicateUser", description: "Invalid credentials.");
        }
    }
}