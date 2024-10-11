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
    public class ReservationMapProfile : Profile
    {
        public ReservationMapProfile()
        {
            CreateMap<AddResBO, Reservation>();
            CreateMap<Reservation, ReadResClientBo>()
                .ForMember(d => d.RES_Id, opt => opt.MapFrom(s => s.RES_Id))
                .ForMember(d => d.RES_DateReservation, opt => opt.MapFrom(s => s.RES_DateReservation))
                .ForMember(d => d.RES_DateDebut, opt => opt.MapFrom(s => s.RES_DateDebut))
                .ForMember(d => d.RES_DateFin, opt => opt.MapFrom(s => s.RES_DateFin))
                .ForMember(d => d.PLA_Etage, opt => opt.MapFrom(s => s.PlaceParking.PLA_Etage))
                .ForMember(d => d.PLA_NumeroPlace, opt => opt.MapFrom(s => s.PlaceParking.PLA_NumeroPlace))
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.PlaceParking.ParkingId))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.PlaceParking.Parking.PARK_Nom))
                .ForMember(d => d.ADRS_Numero, opt => opt.MapFrom(s => s.PlaceParking.Parking.Adresse.ADRS_Numero))
                .ForMember(d => d.ADRS_NomRue, opt => opt.MapFrom(s => s.PlaceParking.Parking.Adresse.ADRS_NomRue))
                .ForMember(d => d.ADRS_Ville, opt => opt.MapFrom(s => s.PlaceParking.Parking.Adresse.ADRS_Ville));

            CreateMap<UpdateClientResBO, Reservation>();
            CreateMap<Reservation, ReadReservationParkingBO>()
                .ForMember(d => d.RES_Id, opt => opt.MapFrom(s => s.RES_Id))
                .ForMember(d => d.RES_DateDebut, opt => opt.MapFrom(s => s.RES_DateDebut))
                .ForMember(d => d.RES_DateFin, opt => opt.MapFrom(s => s.RES_DateFin))
                .ForMember(d => d.PLA_Id, opt => opt.MapFrom(s => s.PlaceId))
                .ForMember(d => d.PERS_Nom, opt => opt.MapFrom(s => s.Client.Cli.PERS_Nom))
                .ForMember(d => d.PERS_Prenom, opt => opt.MapFrom(s => s.Client.Cli.PERS_Prenom))
                .ForMember(d => d.PERS_Id, opt => opt.MapFrom(s => s.Client.Cli.PERS_Id));
        }
    }
}
