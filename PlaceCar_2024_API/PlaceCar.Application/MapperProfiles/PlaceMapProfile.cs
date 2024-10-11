using AutoMapper;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.MapperProfiles
{
    public class PlaceMapProfile : Profile
    {
        public PlaceMapProfile()
        {
            CreateMap<PlaceParking, ReadPlacesLibreBO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.ParkingId))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.Parking.PARK_Nom))
                .ForMember(d => d.PLA_Id, opt => opt.MapFrom(s => s.PLA_Id))
                .ForMember(d => d.PLA_Etage, opt => opt.MapFrom(s => s.PLA_Etage))
                .ForMember(d => d.PLA_NumeroPlace, opt => opt.MapFrom(s => s.PLA_NumeroPlace));
        }
    }
}
