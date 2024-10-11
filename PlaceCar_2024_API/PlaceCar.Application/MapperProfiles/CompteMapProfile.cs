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
    public class CompteMapProfile : Profile
    {
        public CompteMapProfile()
        {
            CreateMap<AddCompteBo, CompteBank>();

            CreateMap<CompteBank, ReadCompteCliBO>()
                .ForMember(d => d.CB_Id, opt => opt.MapFrom(s => s.CB_Id))
                .ForMember(d => d.CB_Nom, opt => opt.MapFrom(s => s.CB_Nom))
                .ForMember(d => d.CB_NumCompte, opt => opt.MapFrom(s => s.CB_NumCompte))
                .ForMember(d => d.CB_Date, opt => opt.MapFrom(s => s.CB_Date))
                .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.ClientId))
                .ForMember(d => d.PERS_Nom, opt => opt.MapFrom(s => s.Client.Cli.PERS_Nom))
                .ForMember(d => d.PERS_Prenom, opt => opt.MapFrom(s => s.Client.Cli.PERS_Prenom));

        }
    }
}
