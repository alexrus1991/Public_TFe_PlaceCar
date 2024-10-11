using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Profiles
{
    public class ParkingProfile : Profile
    {
        public ParkingProfile()
        {
            CreateMap<parkingDto, ParkingBO>();

            CreateMap<ReadParkVilleBO, ReadParkVilleDTO>();

            CreateMap<ParkingEmpWorkBo,ReadParkEmpWorkDTO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PARK_Id))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom));

            CreateMap<ReadAllParkingsBO, ReadAllParkingsDTO>();

            CreateMap<ReadParkingBO, ReadParkingDTO>();
        }
    }
}
