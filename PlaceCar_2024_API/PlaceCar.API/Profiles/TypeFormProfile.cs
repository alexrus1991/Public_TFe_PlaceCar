using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class TypeFormProfile : Profile
    {
        public TypeFormProfile()
        {
            CreateMap<AddFormuleTypeDTO, AddTypeFormBO>();

            CreateMap<ReadTypeBO, ReadTypeFormDTO>();
        }
    }
}
