using System.Threading.Tasks;
using AirlineSendAgent.Dtos;
using AirlineSendAgent.Models;

namespace AirlineSendAgent.Client
{
    public interface IWebhookClient
    {
        Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto);
        Task SendWebhookSecret(WebhookSecret webhookSecret);
    }
}