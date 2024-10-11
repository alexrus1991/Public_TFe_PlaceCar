using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<ReadPlacesLibreBO, ReadPlaceLibDTO>();
            CreateMap<PlaceStatusBO, PlaceStatusDto>();
        }
    }
}
