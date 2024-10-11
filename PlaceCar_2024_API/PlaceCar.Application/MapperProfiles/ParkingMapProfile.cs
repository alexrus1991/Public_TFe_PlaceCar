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
    public class ParkingMapProfile : Profile
    {
        public ParkingMapProfile()
        {
            CreateMap<ParkingBO, ParkingEntity>()
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom))
                .ForMember(d => d.PARK_NbEtages, opt => opt.MapFrom(s => s.PARK_NbEtages))
                .ForMember(d => d.PARK_NbPlaces, opt => opt.MapFrom(s => s.PARK_NbPlaces));

            CreateMap<ParkingBO, Adresse>()
                .ForMember(d => d.ADRS_Numero, opt => opt.MapFrom(s => s.ADRS_Numero))
                .ForMember(d => d.ADRS_NomRue, opt => opt.MapFrom(s => s.ADRS_NomRue))
                .ForMember(d => d.ADRS_Ville, opt => opt.MapFrom(s => s.ADRS_Ville))
                .ForMember(d => d.ADRS_Latitude, opt => opt.MapFrom(s => s.ADRS_Latitude))
                .ForMember(d => d.ADRS_Longitude, opt => opt.MapFrom(s => s.ADRS_Longitude))
                .ForMember(d => d.PaysId, opt => opt.MapFrom(s => s.PAYS_Id));

            //CreateMap<ParkingBO, PaysEntity>()
            //    .ForMember(d => d.PAYS_Id, opt => opt.MapFrom(s => s.PAYS_Id));
            CreateMap<PlaceParking, ReadPlacesLibreBO>()
             .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.ParkingId))
             .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.Parking.PARK_Nom))
             .ForMember(d => d.PLA_Id, opt => opt.MapFrom(s => s.PLA_Id))
             .ForMember(d => d.PLA_Etage, opt => opt.MapFrom(s => s.PLA_Etage))
             .ForMember(d => d.PLA_NumeroPlace, opt => opt.MapFrom(s => s.PLA_NumeroPlace));

            CreateMap<ParkingEntity, ReadParkVilleBO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PARK_Id))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom))
                .ForMember(d => d.PARK_NbEtages, opt => opt.MapFrom(s => s.PARK_NbEtages))
                .ForMember(d => d.PARK_NbPlaces, opt => opt.MapFrom(s => s.PARK_NbPlaces))
                .ForMember(d => d.ADRS_Id, opt => opt.MapFrom(s => s.Adresse.ADRS_Id))
                .ForMember(d => d.ADRS_Numero, opt => opt.MapFrom(s => s.Adresse.ADRS_Numero))
                .ForMember(d => d.ADRS_NomRue, opt => opt.MapFrom(s => s.Adresse.ADRS_NomRue))
                .ForMember(d => d.ADRS_Ville, opt => opt.MapFrom(s => s.Adresse.ADRS_Ville))
                .ForMember(d => d.ADRS_Latitude, opt => opt.MapFrom(s => s.Adresse.ADRS_Latitude))
                .ForMember(d => d.ADRS_Longitude, opt => opt.MapFrom(s => s.Adresse.ADRS_Longitude))
                .ForMember(d => d.PAYS_Nom, opt => opt.MapFrom(s => s.Adresse.Pays.PAYS_Nom));

            CreateMap<ParkingEntity, ParkingEmpWorkBo>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PARK_Id))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom));

            CreateMap<ParkingEntity, ReadAllParkingsBO>()
               .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PARK_Id))
               .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom))
               .ForMember(d => d.PARK_NbEtages, opt => opt.MapFrom(s => s.PARK_NbEtages))
               .ForMember(d => d.PARK_NbPlaces, opt => opt.MapFrom(s => s.PARK_NbPlaces))
               .ForMember(d => d.ADRS_Ville, opt => opt.MapFrom(s => s.Adresse.ADRS_Ville))
               .ForMember(d => d.PAYS_Nom, opt => opt.MapFrom(s => s.Adresse.Pays.PAYS_Nom));

            CreateMap<ParkingEntity, ReadParkingBO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PARK_Id))
               .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PARK_Nom))
               .ForMember(d => d.PARK_NbEtages, opt => opt.MapFrom(s => s.PARK_NbEtages))
               .ForMember(d => d.PARK_NbPlaces, opt => opt.MapFrom(s => s.PARK_NbPlaces));
        }
    }
}
