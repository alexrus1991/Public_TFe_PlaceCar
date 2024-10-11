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
    public class EmpMapProfile : Profile
    {
        public EmpMapProfile()
        {
            CreateMap<AddEmpBO, Personne>();
            CreateMap<EmployeWorkOn, EmpWorkOnBO>()
                .ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.Parking.PARK_Id))
                .ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.Parking.PARK_Nom))
                .ForMember(d => d.Emp_Pers_Id, opt => opt.MapFrom(s => s.Emp_Pers_Id))
                .ForMember(d => d.PERS_Nom, opt => opt.MapFrom(s => s.Employee.EmpPers.PERS_Nom))
                .ForMember(d => d.PERS_Prenom, opt => opt.MapFrom(s => s.Employee.EmpPers.PERS_Prenom))
                .ForMember(d => d.PERS_DateNaissance, opt => opt.MapFrom(s => s.Employee.EmpPers.PERS_DateNaissance))
                .ForMember(d => d.PERS_Email, opt => opt.MapFrom(s => s.Employee.EmpPers.PERS_Email));

            //CreateMap<Employee, EmpWorkOnBO>()
            //    //.ForMember(d => d.PARK_Id, opt => opt.MapFrom(s => s.EmployeWorkOns))
            //    //.ForMember(d => d.PARK_Nom, opt => opt.MapFrom(s => s.EmployeWorkOns))
            //    .ForMember(d => d.Emp_Pers_Id, opt => opt.MapFrom(s => s.Emp_Pers_Id))
            //    .ForMember(d => d.PERS_Nom, opt => opt.MapFrom(s => s.EmpPers.PERS_Nom))
            //    .ForMember(d => d.PERS_Prenom, opt => opt.MapFrom(s => s.EmpPers.PERS_Prenom))
            //    .ForMember(d => d.PERS_DateNaissance, opt => opt.MapFrom(s => s.EmpPers.PERS_DateNaissance))
            //    .ForMember(d => d.PERS_Email, opt => opt.MapFrom(s => s.EmpPers.PERS_Email));


        }
    }
}
