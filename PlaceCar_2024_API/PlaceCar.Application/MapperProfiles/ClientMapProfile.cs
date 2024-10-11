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
    public class ClientMapProfile : Profile
    {
        public ClientMapProfile()
        {
            CreateMap<PersonneBo, Personne>();
            CreateMap<Personne, Client>();

            CreateMap<UpClientBO, Personne>() 
                .ForMember(d => d.PERS_Password, opt => opt.MapFrom(s => s.pwd))
                .ForMember(d => d.PERS_Id, opt => opt.MapFrom(s => s.Id));

            CreateMap<Client, ReadClientBo>()
                .ForMember(d => d.PERS_Nom, opt => opt.MapFrom(s => s.Cli.PERS_Nom))
                .ForMember(d => d.PERS_Prenom, opt => opt.MapFrom(s => s.Cli.PERS_Prenom))
                .ForMember(d => d.PERS_DateNaissance, opt => opt.MapFrom(s => s.Cli.PERS_DateNaissance))
                .ForMember(d => d.PERS_Email, opt => opt.MapFrom(s => s.Cli.PERS_Email));





        }
    }
}
