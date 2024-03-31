using Application.Notification.Commands.Add;
using Application.Notification.Common;
using Contract.Notification;
using Mapster;

namespace API.Mapping
{
    public class NotificationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<NotificationResult, ListNotificationResponse>()
                .Map(dest => dest.Id, src => src.Notifications.Id)
                .Map(dest => dest.Username, src => src.Notifications.Username)
                .Map(dest => dest.Message, src => src.Notifications.Message)
                .Map(dest => dest.MessageType, src => src.Notifications.MessageType)
                .Map(dest => dest.NotificationDateTime, src => src.Notifications.NotificationDateTime);

            config.NewConfig<AddNotificationCommand, AddNotificationRequest>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.MessageType, src => src.MessageType)
                .Map(dest => dest.NotificationDateTime, src => src.NotificationDateTime);
        }
    }
}