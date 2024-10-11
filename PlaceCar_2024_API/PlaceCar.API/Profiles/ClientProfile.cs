using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<AddClientDto, PersonneBo>();
            CreateMap<UpdatePresonCLiDto,UpClientBO>();

            CreateMap<ReadClientBo,ReadClientDto>();
        }
    }
}
