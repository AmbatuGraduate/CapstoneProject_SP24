using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Notification
{
    public record ListNotificationResponse
    (
        Guid Id,
        string Username,
        string Message,
        string MessageType,
        string NotificationDateTime
    );
}
