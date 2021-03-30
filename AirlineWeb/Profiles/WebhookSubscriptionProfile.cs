using AirlineWeb.Dtos;
using AirlineWeb.Model;
using AutoMapper;

namespace AirlineWeb.Profiles
{
    public class WebhookSubscriptionProfile : Profile
    {
        public WebhookSubscriptionProfile()
        {
            // source -> target
            CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();
        }
    }
}