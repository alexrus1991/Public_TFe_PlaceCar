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
    public class FactureMapProfile : Profile
    {
        public FactureMapProfile()
        {
            CreateMap<Facture, RaadFactureBO>()
                 .ForMember(d => d.FACT_Id, opt => opt.MapFrom(s => s.FACT_Id))
                  .ForMember(d => d.FACT_Somme, opt => opt.MapFrom(s => s.FACT_Somme))
                   .ForMember(d => d.FACT_Date, opt => opt.MapFrom(s => s.FACT_Date))
                    .ForMember(d => d.RES_Id, opt => opt.MapFrom(s => s.Reservation.RES_Id))
                     .ForMember(d => d.RES_DateReservation, opt => opt.MapFrom(s => s.Reservation.RES_DateReservation))
                      .ForMember(d => d.RES_DateDebut, opt => opt.MapFrom(s => s.Reservation.RES_DateDebut))
                       .ForMember(d => d.RES_DateFin, opt => opt.MapFrom(s => s.Reservation.RES_DateFin));

            CreateMap<Facture, ReadFactureStripeBO>()
                .ForMember(d => d.FACT_Id, opt => opt.MapFrom(s => s.FACT_Id))
                .ForMember(d => d.StripeConfirmStr, opt => opt.MapFrom(s => s.StripeConfirmStr));
        }
    }
}
