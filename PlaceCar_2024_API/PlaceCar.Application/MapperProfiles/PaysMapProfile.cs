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
    public class PaysMapProfile : Profile
    {
        public PaysMapProfile()
        {
            CreateMap<IEnumerable<Adresse>, IEnumerable<Adresse>>().ConvertUsing< AdresseListToListAdresse>();
            CreateMap<PaysEntity, PaysBO>()
            .ForMember(d => d.PAYS_Id, opt => opt.MapFrom(s => s.PAYS_Id))
            .ForMember(d => d.PAYS_Nom, opt => opt.MapFrom(s => s.PAYS_Nom));
            
            //.ForMember(d => d.ADRS_Id, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_Id)) 
            //.ForMember(d => d.ADRS_Numero, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_Numero)) 
            //.ForMember(d => d.ADRS_NomRue, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_NomRue)) 
            //.ForMember(d => d.ADRS_Ville, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_Ville)) 
            //.ForMember(d => d.ADRS_Latitude, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_Latitude)) 
            //.ForMember(d => d.ADRS_Longitude, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().ADRS_Longitude)) 
            //.ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().Parking.PARK_Id)) 
            //.ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().Parking.PARK_Nom)) 
            //.ForMember(d => d.PARK_NbEtages, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().Parking.PARK_NbEtages)) 
            //.ForMember(d => d.PARK_NbPlaces, opt => opt.MapFrom(s => s.Adresses.FirstOrDefault().Parking.PARK_NbPlaces));
            

        }
    }

    public class AdresseListToListAdresse : ITypeConverter<IEnumerable<Adresse>, IEnumerable<Adresse>>
    {
        public IEnumerable<Adresse> Convert(IEnumerable<Adresse> source, IEnumerable<Adresse> destination, ResolutionContext context)
        {
            List<Adresse> lesAdresse = new List<Adresse>();
            foreach (Adresse item in source)
            {
                Adresse converti = context.Mapper.Map<Adresse, Adresse>(item);
                lesAdresse.Add(converti);
            }
            return lesAdresse;
        }
    }
}
