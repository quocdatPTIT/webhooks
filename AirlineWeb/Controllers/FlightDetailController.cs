using System;
using System.Threading.Tasks;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.MessageBus;
using AirlineWeb.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController: ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public FlightDetailController(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _context = context;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet("get-flight-detail-by-code/{flightCode}", Name = "GetFlightDetailByCode")]
        public async Task<ActionResult<FlightDetailReadDto>> GetFlightDetailByCode(string flightCode)
        {
            var flight = await _context.FlightDetails.FirstOrDefaultAsync(f => f.FlightCode == flightCode);
            if (flight is null) return await Task.FromResult(NotFound());
            return await Task.FromResult(Ok(_mapper.Map<FlightDetailReadDto>(flight)));
        }

        [HttpPost("create-flight-detail")]
        public async Task<ActionResult<FlightDetailReadDto>> CreateFlightDetail(
            FlightDetailCreateDto flightDetailCreateDto)
        {
            var flight =
                await _context.FlightDetails.FirstOrDefaultAsync(f => f.FlightCode == flightDetailCreateDto.FlightCode);
            if (flight is not null) return await Task.FromResult(NoContent());
            var flightDetailModel = _mapper.Map<FlightDetail>(flightDetailCreateDto);
            try
            {
                await _context.AddAsync(flightDetailModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(ex.Message));
            }

            var response = _mapper.Map<FlightDetailReadDto>(flightDetailModel);
            return CreatedAtRoute(
                routeName: "GetFlightDetailByCode",
                routeValues: new {flightCode = response.FlightCode},
                value: response
            );
        }

        [HttpPut("update-flight-detail/{id}")]
        public async Task<ActionResult> UpdateFlightDetail(int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            var flight = await _context.FlightDetails.FirstOrDefaultAsync(f => f.Id == id);
            if (flight is null) return await Task.FromResult(NotFound());

            decimal oldPrice = flight.Price;

            _mapper.Map(flightDetailUpdateDto, flight);

            try
            {
                await _context.SaveChangesAsync();

                if (oldPrice != flight.Price)
                {
                    Console.WriteLine("Price changed - Place message on bus");
                    var message = new NotificationMessageDto
                    {
                        WebhookType = "pricechange",
                        OldPrice = oldPrice,
                        NewPrice = flight.Price,
                        FlightCode = flight.FlightCode
                    };
                    _messageBusClient.SendMessage(message);
                }
                else
                {
                    Console.WriteLine("No price change");
                }
                
                return await Task.FromResult(NoContent());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}