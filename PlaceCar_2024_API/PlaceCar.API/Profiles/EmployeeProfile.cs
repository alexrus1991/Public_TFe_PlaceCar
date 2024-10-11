using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<AddEmplDto, AddEmpBO>();
               

            CreateMap<EmpWorkOnBO,ReadEmpWorkInDTO>();
        }
    }
}
