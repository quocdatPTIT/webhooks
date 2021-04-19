using AirlineWeb.Dtos;

namespace AirlineWeb.MessageBus
{
    public interface IMessageBusClient
    {
        void SendMessage(NotificationMessageDto notificationMessageDto);
        void SendWebhookSecretMessage(WebhookSecretMessageDto secretMessageDto);
    }
}