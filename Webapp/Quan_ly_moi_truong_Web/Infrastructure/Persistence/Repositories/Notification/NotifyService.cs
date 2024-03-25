﻿using Application.Common.Interfaces.Persistence;
using Microsoft.AspNetCore.SignalR;

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