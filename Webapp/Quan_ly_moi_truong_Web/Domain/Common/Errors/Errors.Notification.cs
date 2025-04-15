using ErrorOr;

namespace Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Notification
        {
            public static Error getNotificationFail = Error.NotFound(
                code: "notification.getNotificationFail", description: "Not Found Notification");
        }
    }
}