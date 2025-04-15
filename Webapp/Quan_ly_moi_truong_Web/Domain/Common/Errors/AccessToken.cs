using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class AccessToken
        {
            public static Error InvalidAccessToken = Error.Validation(
                           code: "auth.DuplicateUser", description: "Invalid Access token.");
        }
    }
}