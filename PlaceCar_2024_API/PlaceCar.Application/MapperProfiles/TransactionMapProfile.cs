using AutoMapper;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PlaceCar.Application.MapperProfiles
{
    public class TransactionMapProfile : Profile
    {
        public TransactionMapProfile()
        {
            CreateMap<AddTransacBO, Trensaction>()
                .ForMember(d => d.TRANS_Communication, opt => opt.MapFrom(s => s.TRANS_Communication))
                .ForMember(d => d.FactureId, opt => opt.MapFrom(s => s.FactureId))
                .ForMember(d => d.TRANS_Somme, opt => opt.MapFrom(s => s.Somme))
                .ForMember(d => d.CompteUnId, opt => opt.MapFrom(s => s.CompteUnId))
                .ForMember(d => d.CompteEntreprise, opt => opt.MapFrom(s => s.Cb_NumCompte_Client));

            CreateMap<Trensaction, ReadTransacBO>()
                .ForMember(d => d.TRANS_Id, opt => opt.MapFrom(s => s.TRANS_Id))
                .ForMember(d => d.TRANS_Somme, opt => opt.MapFrom(s => s.TRANS_Somme))
                .ForMember(d => d.TRANS_Date, opt => opt.MapFrom(s => s.TRANS_Date))
                .ForMember(d => d.TRANS_Communication, opt => opt.MapFrom(s => s.TRANS_Communication))
                .ForMember(d => d.CB_Nom, opt => opt.MapFrom(s => s.CompteUn.CB_Nom))
                .ForMember(d => d.CB_NumCompte, opt => opt.MapFrom(s => s.CompteUn.CB_NumCompte))
                .ForMember(d => d.Nom, opt => opt.MapFrom(s => s.CompteEntrepriseNavigation.Nom));

            CreateMap<Trensaction, ReadDeataiTransacBo>()
            .ForMember(d => d.TransactionId, opt => opt.MapFrom(s => s.TRANS_Id))
            .ForMember(d => d.TransactionSomme, opt => opt.MapFrom(s => s.TRANS_Somme))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(s => s.TRANS_Date))
            .ForMember(d => d.TransactionCommunication, opt => opt.MapFrom(s => s.TRANS_Communication))
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.CompteUn.Client.Cli_Id))
            .ForMember(d => d.ClientPrenom, opt => opt.MapFrom(s => s.CompteUn.Client.Cli.PERS_Prenom))
            .ForMember(d => d.ClientNom, opt => opt.MapFrom(s => s.CompteUn.Client.Cli.PERS_Nom))
            .ForMember(d => d.ClientComptetId, opt => opt.MapFrom(s => s.CompteUnId))
            .ForMember(d => d.ClientComptetNumero, opt => opt.MapFrom(s => s.CompteUn.CB_NumCompte))
            .ForMember(d => d.BeneficierNom, opt => opt.MapFrom(s => s.CompteEntrepriseNavigation.Nom))
            .ForMember(d => d.BeneficiereCompteNumero, opt => opt.MapFrom(s => s.CompteEntrepriseNavigation.Cb_NumCompte))
            .ForMember(d => d.ParkingId, opt => opt.MapFrom(s => s.Facture.Reservation.PlaceParking.Parking.PARK_Id))
            .ForMember(d => d.ParkingNom, opt => opt.MapFrom(s => s.Facture.Reservation.PlaceParking.Parking.PARK_Nom));

        }
    }
}
