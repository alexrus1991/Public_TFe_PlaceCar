using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<AddResDTO,AddResBO>();

            CreateMap<ReadResClientBo,ReadResClientDTO>();

            CreateMap<UpdateClientResDTO, UpdateClientResBO>();

            CreateMap<UpdateClientResBO, UpdateClientResDTO>();

            CreateMap<ReadReservationParkingBO,ReadReservationParkingDTO>();
        }
    }
}
