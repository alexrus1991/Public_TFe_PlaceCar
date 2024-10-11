using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class FactureProfile : Profile
    {
        public FactureProfile()
        {
            CreateMap<RaadFactureBO, ReadFactureDTO>();
            CreateMap<ReadFactureStripeBO, ReadFactureStripeDto>();
        }
    }
}
