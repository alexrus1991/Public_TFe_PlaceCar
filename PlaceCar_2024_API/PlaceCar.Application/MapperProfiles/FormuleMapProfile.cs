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
    public class FormuleMapProfile : Profile
    {
        public FormuleMapProfile()
        {
            CreateMap<AddFormuleBO, FormuleDePrix>()
                .ForMember(d=>d.FORM_Prix,opt => opt.MapFrom(s=>s.Prix));

            CreateMap<FormuleDePrix, ReadForulesParkingBO>()
                .ForMember(d => d.FORM_Id, opt => opt.MapFrom(s => s.FORM_Id))
                .ForMember(d => d.FORM_Prix, opt => opt.MapFrom(s => s.FORM_Prix))
                .ForMember(d => d.FORM_Title, opt => opt.MapFrom(s => s.FormuleDePrixType.FORM_Title))
                .ForMember(d => d.FORM_Type_Description, opt => opt.MapFrom(s => s.FormuleDePrixType.FORM_Type_Description));
        }
    }
}
