using Application.Common.Interfaces.Persistence.Notifiy;
using Domain.Entities.Notification;
using Infrastructure.Persistence.Repositories.Notification.Hubs;
using Microsoft.AspNetCore.SignalR;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace Infrastructure.Persistence.Repositories.Notification.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Notifications> tableDependency;
        private IHubContext<NotifyHub> hubContext;

        public SubscribeNotificationTableDependency(IHubContext<NotifyHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public void SubscribeTableDependency()
        {
            Console.WriteLine("Start sql dependency");
            tableDependency = new SqlTableDependency<Notifications>("Server=20.255.186.117,1433;Initial Catalog=UrbanSanitationDB;Persist Security Info=False;User ID=ad;Password=Urban3579;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;");
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"ERROR {nameof(Notifications)}: " + e.Error.Message);
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Notifications> e)
        {
            Console.WriteLine("Testing add change msg");
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notify = e.Entity;
                System.Diagnostics.Debug.WriteLine("user: " + notify.Username);
                // Message to all
                if (notify.MessageType.ToLower().Equals("all"))
                {
                    await hubContext.Clients.All.SendAsync("ReceivedNotification", notify.Message);
                }
                // Message to single user
                else
                {
                    await hubContext.Clients.User(notify.Username).SendAsync("ReceivedPersonalNotification", notify.Message, notify.Username);
                }
            }
        }
    }
}