﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface INotifyService
    {
        Task SendMessage(string userName, string messageContent);
    }
}
