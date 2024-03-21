using Application.Common.Interfaces.Persistence;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Notification
{

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<NotifyHub> _hubContext;

        public NotifyService(IHubContext<NotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string userName, string messageContent)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", userName, messageContent);
        }

    }
}
