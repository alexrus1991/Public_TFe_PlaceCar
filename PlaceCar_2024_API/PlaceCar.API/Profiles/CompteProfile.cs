using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Profiles
{
    public class CompteProfile : Profile
    {
        public CompteProfile()
        {
            CreateMap<AddCompteDTO, AddCompteBo>();
            CreateMap<ReadCompteCliBO, ReadCompteCliDTO>();
        }

       
    }
}
