using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class TrensactionService : ITransactionService
    {
        private readonly IFactureService _factureService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public TrensactionService(IFactureService factureService, IUnitOfWork unitOfWork, IMapper mapper2)
        {
            _factureService = factureService;
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        //public async Task<ReadTransacBO> AddTrensaction(AddTransacBO transacBO)
        //{
        //    try
        //    {
        //        ReadTransacBO trsReponce = new ReadTransacBO();
        //        var facture  = await unitOfWork.Facture.GetFactureById(transacBO.FactureId);
        //        Trensaction t = new Trensaction();
        //        if (transacBO == null) { throw new ArgumentNullException("La facture donnée est vide !!"); }
        //        else
        //        {
        //            // client de la facture = client du compte
        //            var compte = await unitOfWork.Compte.GetCompteById(transacBO.CompteUnId);
        //            if (compte == null){ throw new ArgumentNullException("Compte en banque FAUX !!"); }
        //            if (compte.ClientId != facture.Reservation.ClientId && compte.CB_NumCompte == transacBO.Cb_NumCompte_Client) 
        //            { throw new ArgumentNullException("La compte client n'appartient pas au compte du client associé à la réservation !!"); }
        //            else
        //            {
        //                var tr = mapper2.Map<Trensaction>(transacBO);
        //                var info = await unitOfWork.InfoEntrprise.GetInfoEntreprise();
        //                tr.CompteEntreprise = info.Nom;

        //               if(transacBO.Preference == true)
        //               {
        //                    var parking = await unitOfWork.Parking.GetParkingById(facture.Reservation.PlaceParking.ParkingId);
        //                    Preferences preference = new Preferences
        //                    {
        //                        PlaceId = facture.Reservation.PlaceId,
        //                        ParkingId = parking.PARK_Id,
        //                        ClientId = facture.Reservation.ClientId
        //                    };

        //                    await unitOfWork.Preferances.AddPreferance(preference);
        //               }                      

        //                t = await unitOfWork.Trensaction.AddTransaction(tr);
        //                facture.TransactionId = t.TRANS_Id;
        //                await unitOfWork.Facture.UpdateFacture(facture);
        //                trsReponce = mapper2.Map<ReadTransacBO>(t);
        //            }

        //            await unitOfWork.SaveAsync();

        //        }
        //        return trsReponce;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task AddTrensaction(AddTransacBO transacBO)
        {
            try
            {
                var facture = await unitOfWork.Facture.GetFactureById(transacBO.FactureId);
               if(facture.TransactionId != 0)
                {
                    var ttrans = await unitOfWork.Trensaction.GetTransactionById(facture.TransactionId);
                    var rep =await unitOfWork.Trensaction.DeleteTransaction(ttrans);
                    facture.TransactionId = 0;
                    await unitOfWork.Facture.UpdateFacture(facture);
                    await unitOfWork.SaveAsync();
               }
                Trensaction t = new Trensaction();
                if (transacBO == null) { throw new ArgumentNullException("La facture donnée est vide !!"); }
                else
                {
                    // client de la facture = client du compte
                    var compte = await unitOfWork.Compte.GetCompteById(transacBO.CompteUnId);
                    if (compte == null) { throw new ArgumentNullException("Compte en banque FAUX !!"); }
                    if (compte.ClientId != facture.Reservation.ClientId && compte.CB_NumCompte == transacBO.Cb_NumCompte_Client)
                    { throw new ArgumentNullException("La compte client n'appartient pas au compte du client associé à la réservation !!"); }
                    else
                    {
                        var tr = mapper2.Map<Trensaction>(transacBO);
                        var info = await unitOfWork.InfoEntrprise.GetInfoEntreprise();
                        tr.CompteEntreprise = info.Nom;

                        if (transacBO.Preference == true)
                        {
                            var parking = await unitOfWork.Parking.GetParkingById(facture.Reservation.PlaceParking.ParkingId);
                            Preferences preference = new Preferences
                            {
                                PlaceId = facture.Reservation.PlaceId,
                                ParkingId = parking.PARK_Id,
                                ClientId = facture.Reservation.ClientId
                            };

                            await unitOfWork.Preferances.AddPreferance(preference);
                        }

                        t = await unitOfWork.Trensaction.AddTransaction(tr);
                        facture.TransactionId = t.TRANS_Id;
                        await unitOfWork.Facture.UpdateFacture(facture);

                    }

                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ReadTransacBO>> GetTrensactionsClient(int clientId)
        {
            try
            {
                List<ReadTransacBO> lst = new List<ReadTransacBO>();    
                var client = await unitOfWork.Client.GetClientById(clientId);

                if(client == null) { throw new ArgumentNullException("Le client demandé est vide !!"); }
                else
                {
                    List<Trensaction> trsList = await unitOfWork.Trensaction.GetAllTransactionsClient(clientId);
                    lst = mapper2.Map<List<ReadTransacBO>>(trsList);
                    await unitOfWork.SaveAsync();
                }
                return lst;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ReadDeataiTransacBo>> GetAllTrensactions(DateTime date)
        {
            try
            {
                List<ReadDeataiTransacBo> lst = new List<ReadDeataiTransacBo>();
                        
                List<Trensaction> trsList = await unitOfWork.Trensaction.GetAllTransactionsPlacecar(date);
                lst = mapper2.Map<List<ReadDeataiTransacBo>>(trsList);
                
                await unitOfWork.SaveAsync();
               
                return lst;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateTrensaction(int factureId, string description)// on a besoin de la transactionId pour la modifier!!!
        {
            try
            {
                var facture = await unitOfWork.Facture.GetFactureById(factureId);
                var trs = await unitOfWork.Trensaction.GetTransactionById(facture.TransactionId);
               if (trs == null) { throw new ArgumentNullException("La Transaction demandée est introuvable !!"); }
               else
               {
                    trs.TRANS_Communication = description;
                    var transac = await unitOfWork.Trensaction.UpdateTransactionNoValide(trs);
                    
                }
               await unitOfWork.SaveAsync();               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public async Task<AddTransacBO> DeleteTransactionFailure(AddTransacBO transacBO)
        //{
        //    try
        //    {
        //        AddTransacBO trs = new AddTransacBO();
        //        if (transacBO == null) { throw new ArgumentException("Le Parametre TransactionBO est vide !!."); }
        //        else
        //        {
        //            var t = mapper2.Map<Trensaction>(transacBO);
        //            var transactionExiste = await unitOfWork.Trensaction.GetTransactionById(t.TRANS_Id);
        //            var facture = await unitOfWork.Facture.GetFactureById(t.FactureId);
        //            if (transactionExiste == null) { throw new ArgumentException("La transaction demandé n'existe pas !!."); return transacBO; }
        //            if (facture == null) { throw new ArgumentException("La Facture n'est pas associée à la Transaction donnée !!."); return transacBO; }
                    
        //            else
        //            {
        //                var rep = await unitOfWork.Trensaction.DeleteTransaction(transactionExiste);
        //                facture.TransactionId = 0;
        //                await unitOfWork.Facture.UpdateFacture(facture);
        //                trs = mapper2.Map<AddTransacBO>(rep);

        //            }

        //            await unitOfWork.SaveAsync();
        //        }
        //        return trs;


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
