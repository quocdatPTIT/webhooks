using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineSendAgent.Dtos;
using AirlineSendAgent.Models;

namespace AirlineSendAgent.Client
{
    public class WebhookClient : IWebhookClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebhookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto)
        {
            Console.WriteLine("Inside me");

            var serializedPayload = JsonSerializer.Serialize(flightDetailChangePayloadDto);
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailChangePayloadDto.WebhookURI);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serializedPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                using var response = await httpClient.SendAsync(request);
                Console.WriteLine("Success");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unsuccessful {e.Message}");
            }
        }

        public async Task SendWebhookSecret(WebhookSecret webhookSecret)
        {
            var serializedPayload = JsonSerializer.Serialize(webhookSecret);
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post,
                "http://localhost:7500/api/WebhookSecret/create-webhook-secret");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serializedPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                using var response = await httpClient.SendAsync(request);
                Console.WriteLine("Success");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unsuccessful {e.Message}");
            }
        }
    }
}