﻿using Domain.Entities.Notification;

namespace Application.Common.Interfaces.Persistence.Notifiy
{
    public interface INotificationRepository
    {
        List<Notifications> GetlNotifications();

        Task<List<Notifications>> GetNotificationsByUseranme(string username);

        Notifications GetlNotification(Guid Id);

        Notifications CreateNotification(Notifications notification);
    }
}