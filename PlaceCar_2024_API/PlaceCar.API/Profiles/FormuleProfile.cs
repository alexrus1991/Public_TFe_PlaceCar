using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Profiles
{
    public class FormuleProfile : Profile
    {
        public FormuleProfile()
        {
            
            CreateMap<AddFormPrixDTO, AddFormuleBO>();

            CreateMap<ReadForulesParkingDTO, ReadForulesParkingBO>();

            CreateMap<FormuleOptionDTO,ReadFormuleOptionBO>();
        }
    }
}
