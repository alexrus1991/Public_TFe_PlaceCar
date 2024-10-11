using AutoMapper;
using PlaceCar.API.Models;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<AddTransacDTO, AddTransacBO>();

            CreateMap<ReadTransacBO, ReadTransacDTO>()
                .ForMember(d => d.TRANS_Id, opt => opt.MapFrom(s => s.TRANS_Id))
                .ForMember(d => d.TRANS_Somme, opt => opt.MapFrom(s => s.TRANS_Somme))
                .ForMember(d => d.TRANS_Date, opt => opt.MapFrom(s => s.TRANS_Date))
                .ForMember(d => d.TRANS_Communication, opt => opt.MapFrom(s => s.TRANS_Communication))
                .ForMember(d => d.CB_Nom, opt => opt.MapFrom(s => s.CB_Nom))
                .ForMember(d => d.CB_NumCompte, opt => opt.MapFrom(s => s.CB_NumCompte))
                .ForMember(d => d.Nom, opt => opt.MapFrom(s => s.Nom));

            CreateMap<ReadDeataiTransacBo , ReadDeataiTransacDTO>()
            .ForMember(d => d.TransactionId, opt => opt.MapFrom(s => s.TransactionId))
            .ForMember(d => d.TransactionSomme, opt => opt.MapFrom(s => s.TransactionSomme))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(s => s.TransactionDate))
            .ForMember(d => d.TransactionCommunication, opt => opt.MapFrom(s => s.TransactionCommunication))
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.ClientId))
            .ForMember(d => d.ClientPrenom, opt => opt.MapFrom(s => s.ClientPrenom))
            .ForMember(d => d.ClientNom, opt => opt.MapFrom(s => s.ClientNom))
            .ForMember(d => d.ClientComptetId, opt => opt.MapFrom(s => s.ClientComptetId))
            .ForMember(d => d.ClientComptetNumero, opt => opt.MapFrom(s => s.ClientComptetNumero))
            .ForMember(d => d.BeneficierNom, opt => opt.MapFrom(s => s.BeneficierNom))
            .ForMember(d => d.BeneficiereCompteNumero, opt => opt.MapFrom(s => s.BeneficiereCompteNumero))
            .ForMember(d => d.ParkingId, opt => opt.MapFrom(s => s.ParkingId))
            .ForMember(d => d.ParkingNom, opt => opt.MapFrom(s => s.ParkingNom));
        }
    }
}
