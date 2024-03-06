using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials = Error.Validation(
                           code: "auth.DuplicateUser", description: "Invalid credentials.");

            public static Error ExpireRefreshToken = Error.NotFound(
                code: "auth.ExpireRefreshToken", description: "The Refresh Token Has Expired");
        }
    }
}