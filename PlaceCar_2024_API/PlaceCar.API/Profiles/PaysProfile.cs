using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class PaysProfile : Profile
    {
        public PaysProfile()
        {
            CreateMap<PaysBO, ReadPaysDTO>();
        }

        
    }
}
