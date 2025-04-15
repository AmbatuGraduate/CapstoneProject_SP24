using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class AddGoogle
        {
            public static Error AddGoogleUserFail = Error.Failure(
                code: "add.AddGoogleUser", description: "Failed to add user.");

            public static Error AddGoogleCalendarFail = Error.Failure(
                code: "add.AddCalendar", description: "Failed to add calendar.");
        }
    }
}