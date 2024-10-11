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
    public class FormTypeMapProfile : Profile
    {
        public FormTypeMapProfile()
        {
            CreateMap<AddTypeFormBO, FormuleDePrixType>();

            CreateMap<FormuleDePrixType, ReadTypeBO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FORM_Type_Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.FORM_Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.FORM_Type_Description));
        }
    }
}
