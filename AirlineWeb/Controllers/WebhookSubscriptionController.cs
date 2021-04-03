using System;
using System.Threading.Tasks;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("get-subscription-by-code/{secret}", Name = "GetSubscriptionBySecret")]
        public async Task<ActionResult<WebhookSubscriptionReadDto>> GetSubscriptionBySecret(string secret)
        {
            var subscription = await _context.WebhookSubscriptions.FirstOrDefaultAsync(s => s.Secret == secret);
            if (subscription is null) return await Task.FromResult(NotFound());
            return await Task.FromResult(Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription)));
        }
        
        [HttpPost("create-subscription")]
        public async Task<ActionResult<WebhookSubscriptionReadDto>> CreateSubscription(
            WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            var subscription =
                await _context.WebhookSubscriptions.FirstOrDefaultAsync(s =>
                    s.WebhookURI == webhookSubscriptionCreateDto.WebhookURI);
            if (subscription is not null) return await Task.FromResult(NoContent());
            subscription = _mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);
            subscription.Secret = Guid.NewGuid().ToString();
            subscription.WebhookPublisher = "AmTech";
            try
            {
                await _context.AddAsync(subscription);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(ex.Message));
            }
            
            var response = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
            return CreatedAtRoute(
                routeName: "GetSubscriptionBySecret",
                routeValues: new {secret = response.Secret},
                value: response);
        }
    }
}