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
    public class FlightDetailController: ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        public FlightDetailController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                await _context.AddAsync(flight);
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
            _mapper.Map(flightDetailUpdateDto, flight);
            await _context.SaveChangesAsync();
            return await Task.FromResult(NoContent());
        }
    }
}