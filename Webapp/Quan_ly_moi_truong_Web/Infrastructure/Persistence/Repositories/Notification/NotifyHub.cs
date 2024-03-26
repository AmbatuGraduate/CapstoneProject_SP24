using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories.Notification
{
    public class NotifyHub : Hub
    {
        public async Task SendMessage()
        {
            var chatService = Context.GetHttpContext().RequestServices.GetService<NotifyService>();
            await chatService.AutoCreateCalendar();
        }
    }
}