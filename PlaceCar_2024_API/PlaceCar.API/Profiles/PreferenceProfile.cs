using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Profiles
{
    public class PreferenceProfile : Profile
    {
        public PreferenceProfile()
        {
            CreateMap<AddPrefDTO, AddPrefBO>();

            CreateMap<ReadPrefBO,ReadPrefDTO>();

            CreateMap<AddPrefBO, AddPrefDTO>();
        }
    }
}
