using AutoMapper;
using AutoMapper.Features;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.Exceptions.Business;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class FactureService : IFactureService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public FactureService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        //public async Task<Facture> AddFacture(Reservation reservation)
        //{
        //    try
        //    {
        //        Facture facture;
        //        if (reservation == null) { throw new ArgumentException("L'objet reservation est vide !!"); }
        //        else
        //        {

        //            if (reservation.RES_DureeTotal_Initiale == 0)// si 0 : formId à L'heure donc prix 0 avant la cloture
        //            {
        //                facture = new Facture() { FACT_Somme = 0, ReservationId = reservation.RES_Id };
        //            }
        //            else
        //            {
        //                var fPrix = await unitOfWork.Formule.GetFormuleByIdWithType(reservation.FormPrixId);
        //                facture = new Facture() { FACT_Somme = fPrix.FORM_Prix,  ReservationId = reservation.RES_Id };
        //                facture = await unitOfWork.Facture.AddFacture(facture);                     
        //            }
        //            await unitOfWork.SaveAsync();
        //        }
        //        return facture;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        public async Task<Facture> AddFacture(Facture facture)
        {
            try
            {
                if(facture == null) { throw new ArgumentException("La facture est vide !!"); }
                else
                {
                    facture.FACT_Date = DateTime.Now;
                    facture = await unitOfWork.Facture.AddFacture(facture);
                }
                return facture;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<List<RaadFactureBO>> GetFacturesNonPaye(int clientId)
        {
            try
            {
                List<RaadFactureBO> lst = new List<RaadFactureBO>();
                var client = await unitOfWork.Client.GetClientById(clientId);

                if (client == null) { throw new ClientNotFoundExeption(); }
                else
                {
                    List<Facture> factures = await unitOfWork.Facture.GetAllFacturesClient(clientId);
                    lst = mapper2.Map<List<RaadFactureBO>>(factures);
                    await unitOfWork.SaveAsync();

                }
                return lst;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<RaadFactureBO>> GetFacturesPaye(int clientId)
        {
            try
            {
                List<RaadFactureBO> lst = new List<RaadFactureBO>();
                var client = await unitOfWork.Client.GetClientById(clientId);

                if (client == null) { throw new ClientNotFoundExeption(); }
                else
                {
                    List<Facture> factures = await unitOfWork.Facture.GetAllFacturesClientPaye(clientId);
                    lst = mapper2.Map<List<RaadFactureBO>>(factures);
                    await unitOfWork.SaveAsync();

                }
                return lst;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        

        public async Task UpdateFacture(Facture facture, decimal prix)
        {
            try
            {
                Facture f;
                if(facture == null) { throw new ArgumentException("L'Id de facture est vide !!"); }
                else
                {
                    facture.FACT_Somme = prix;
                    await unitOfWork.Facture.UpdateFacture(facture);
                    await unitOfWork.SaveAsync();
                }
                
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task UpdateFactureStripe(int factureId, string stripeString,bool status)
        {
            try
            {
                var facture = await unitOfWork.Facture.GetFactureById(factureId);
                if (facture == null) { throw new ArgumentException("L'Id de facture est incorecte !!"); }
                else
                {
                    if ((stripeString == null && stripeString == " ") && (status == false)) { throw new ArgumentException("La clé Stripe est vide"); }
                    else
                    {
                        facture.StripeConfirmStr = stripeString;
                        facture.Status = status;
                        await unitOfWork.Facture.UpdateFacture(facture);
                        await unitOfWork.SaveAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<StatFacturesBO> GetStatFacturesParJour(int parkingId, DateTime dateDonee)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if(parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                else
                {
                    var rep = await unitOfWork.Facture.GetStatFactures(parking.PARK_Id,dateDonee);
                    if(rep == null) { throw new ArgumentException("Toutes les Factures sont payées !!"); }
                    else
                    {
                        return rep;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReadFactureStripeBO> GetFactureStripe(int id)
        {
            try
            {
                var facture = await unitOfWork.Facture.GetFactureById(id);
                if(facture == null) { throw new ArgumentException("Aucune Facture avec l'identifient indiqué n'as été trouvé"); }
                if(facture.StripeConfirmStr == null || facture.StripeConfirmStr == " ") { throw new ArgumentException("La Clé Stripe est abcente ou n'as pas été attribuée"); }
                else
                {
                    ReadFactureStripeBO FactureStripe = mapper2.Map< ReadFactureStripeBO>(facture);
                    return FactureStripe;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
