using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence.Notifiy
{
    public interface INotifyService
    {
        Task SendToAll(string msg);
        Task SendToUser(string user, string msg);
    }
}
