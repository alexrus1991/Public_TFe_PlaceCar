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
    public class PreferMapProfile : Profile
    {
        public PreferMapProfile()
        {
            CreateMap<AddPrefBO, Preferences>()
                .ForMember(d => d.PlaceId, opt => opt.MapFrom(s => s.PlaceId))
                .ForMember(d => d.ParkingId, opt => opt.MapFrom(s => s.ParkingId))
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.ClientId));

            CreateMap<Preferences, ReadPrefBO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.ParkingId))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.Parking.PARK_Nom))
                .ForMember(d => d.PLA_Etage, opt => opt.MapFrom(s => s.Place.PLA_Etage))
                .ForMember(d => d.PLA_NumeroPlace, opt => opt.MapFrom(s => s.Place.PLA_NumeroPlace))
                .ForMember(d => d.PLA_Id, opt => opt.MapFrom(s => s.Place.PLA_Id));

            CreateMap<Preferences, AddPrefBO>();
        }
    }
}
