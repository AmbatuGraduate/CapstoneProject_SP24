using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Notification
{
    public class NotifyHub : Hub
    {
        public async Task SendMessage(string userName, string messageContent)
        {
            var chatService = Context.GetHttpContext().RequestServices.GetService<NotifyService>();
            await chatService.SendMessage(userName, messageContent);
        }
    }
}
