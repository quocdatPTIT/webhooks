using AirlineWeb.Dtos;
using AirlineWeb.Model;
using AutoMapper;

namespace AirlineWeb.Profiles
{
    public class FlightDetailProfile: Profile
    {
        public FlightDetailProfile()
        {
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
        }
    }
}