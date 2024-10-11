using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.BusinessTools;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.Exceptions.Business;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IFactureService _factureService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper2, IFactureService factureService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
            this._factureService = factureService;
        }

        public async Task DeleteReservation(int resId, int clientId)
        {
            try
            {
                Facture facture = new Facture();

                var client = await unitOfWork.Client.GetClientById(clientId);
                var res = await unitOfWork.Reservation.GetReservationById(resId);

                if(res == null)
                {
                    return;
                }
                
                if (res.ClientId != client.Cli_Id) { throw new ClientReservationExeption(); }
                else if(client == null) { throw new ClientNotFoundExeption(); }
                else if (res == null) { throw new ReservationNotFoundExeption(resId); }
                else
                {
                    var personne = await unitOfWork.Personne.GetPersonneById(client.Cli_Id);
                    if (res.RES_DateDebut <= DateTime.Now) 
                    {                     
                        throw new ArgumentException("La reservation a déjà commencé. !! Vous pouvez uniquement Cloturer cette reservation !!.");
                    }
                    else
                    {
                        var formPrix = await unitOfWork.Formule.GetFormuleById(res.FormPrixId);
                        var formType = await unitOfWork.FormuleType.GetFormTypeById(formPrix.TypeId);
                        var formulesParking = await unitOfWork.Formule.GetFormulesPrixByParkingId(formPrix.ParkingId);

                        if (res.RES_DureeTotal_Initiale == 0) // si reservatio par Heure 
                        {
                            await unitOfWork.Reservation.DeleteReservationClient(res.RES_Id, res.ClientId);
                        }
                        else// Pour le reste on supprime pas lareservation ,On cloture et on calcule le prix
                        {
                            //facture.ReservationId = res.RES_Id;
                            decimal prix = FactureTool.CalculePrixDeleteReservatio(res ,formPrix, formulesParking, personne);
                            facture.FACT_Somme = prix;
                        }
                        
                    }
                    await unitOfWork.SaveAsync();
                }
                var f = await _factureService.AddFacture(facture);
                await unitOfWork.Reservation.UpdatereservationDelete(res.RES_Id, f.FACT_Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        

        //public async Task<Reservation> UpdateClotureReservationClient(int resId) 
        //{
        //    try
        //    {
        //        Facture facture = new Facture();
        //        decimal prix = 0;
        //        var res = await unitOfWork.Reservation.GetReservationById(resId);
        //        if (res == null) { throw new ArgumentException("La reservation n'est pas dans la base de donnée !!."); }
        //        if(res.RES_DateDebut > DateTime.Now)
        //        {
        //             res.RES_Cloture = true;
        //            //DeleteReservation(res.RES_Id, res.ClientId);
        //        }
        //        else
        //        {
        //            facture.ReservationId = res.RES_Id;
        //            var formPrix = await unitOfWork.Formule.GetFormuleById(res.FormPrixId);
        //            var formType = await unitOfWork.FormuleType.GetFormTypeById(formPrix.TypeId);  

        //            if (res.RES_DureeTotal_Initiale == 0 && formType.FORM_Title == "Heure") // si c'est par Heurs Chisie ou Imposée!!!
        //            {

        //                prix = FactureTool.CalculeSommeHeursExacte(res.RES_DateDebut, DateTime.Now, formPrix);
        //                res.RES_DureeTotal_Reele = FactureTool.CalculeTotalHeurs(res.RES_DateDebut, DateTime.Now);
        //                res.RES_DureeTotal_Initiale = res.RES_DureeTotal_Reele;
        //                res.RES_Cloture = true;
        //                facture.FACT_Somme = prix;
        //               // facture.ReservationId = res.RES_Id;
        //                //facture = await _factureService.AddFacture(res);   // On cree la facture avec la reservation Initiale
        //                //res.FactureId = facture.FACT_Id;
        //               // await unitOfWork.SaveAsync();

        //               // await _factureService.UpdateFacture(facture, prix); // On Update la Facture crée Plus Haut dans le programme  
        //            }
        //            else // Pour tout les autres formules On calcule En Jours !!!
        //            {
        //                res.RES_DureeTotal_Reele = ReservationTool.CalculeTempsReel(res.RES_DateDebut, DateTime.Now);
        //                decimal difference = ReservationTool.CalculeDifferenceInit_Reel(res.RES_DureeTotal_Initiale, res.RES_DureeTotal_Reele);
        //                if(difference <= 0) //Si le client n'as pas depassé la date fin, La Facture reste Inchengé 
        //                {
        //                    //var facture = await _factureService.AddFacture(res);   // On cree la facture avec la reservation Initiale
        //                    //res.FactureId = facture.FACT_Id;
        //                    prix = FactureTool.CalculPrix(res.RES_DateDebut, difference, res.RES_DureeTotal_Reele, res.RES_DateReservation, formPrix, formType);
        //                    //prix = FactureTool.CalculeMontantExacte(facture, difference);
        //                    res.RES_Cloture = true;
        //                   // facture.ReservationId = res.RES_Id;
        //                   // await unitOfWork.SaveAsync();

        //                    //await _factureService.UpdateFacture(facture, prix);
        //                }
        //                else // si non on recalcule le prix avec penalité et on Update la facture
        //                {
        //                    //var facture = await _factureService.AddFacture(res);   // On cree la facture avec la reservation Initiale
        //                    //res.FactureId = facture.FACT_Id;
        //                    prix = FactureTool.CalculPrix(res.RES_DateDebut,difference,res.RES_DureeTotal_Reele,res.RES_DateReservation,formPrix,formType);
        //                    facture.FACT_Somme = prix;                  
        //                    res.RES_Cloture = true;

        //                   // await unitOfWork.SaveAsync();

        //                    //await _factureService.UpdateFacture(facture, prix);

        //                }
        //               // await unitOfWork.Reservation.UpdateResCloture(res);
        //            }
        //        }
        //        await unitOfWork.SaveAsync();
        //        var f =  await _factureService.AddFacture(facture);
        //        res.FactureId = f.FACT_Id;
        //        await unitOfWork.Reservation.UpdateResCloture(res);
        //        return res;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public async Task<Reservation> UpdateClotureReservationClient(int resId,int clientId)
        {
            try
            {
                Facture facture = new Facture();
                decimal prix = 0;
                var personne = await unitOfWork.Personne.GetPersonneById(clientId);//pour verifier si le client est aussi un employé

                var res = await unitOfWork.Reservation.GetReservationById(resId);
                if(res.ClientId != personne.Client.Cli_Id) { throw new ClientReservationExeption(); }
                if (res == null) { throw new ReservationNotFoundExeption(resId); }

 
                
                var formPrix = await unitOfWork.Formule.GetFormuleByIdWithType(res.FormPrixId);
                var formType = await unitOfWork.FormuleType.GetFormTypeById(formPrix.TypeId);

                var formulesParking = await unitOfWork.Formule.GetFormulesPrixByParkingId(formPrix.ParkingId);
                if (res.RES_DateDebut > DateTime.Now)//Si reservation n'a pas commencé
                {
 
                    if (res.RES_DureeTotal_Initiale == 0) // si reservatio par Heure 
                    {
                        await unitOfWork.Reservation.DeleteReservationClient(res.RES_Id, res.ClientId);
                    }
                    else// Pour le reste on supprime pas lareservation ,On cloture et on calcule le prix
                    {
                        //facture.ReservationId = res.RES_Id;
                        res.RES_Cloture = true;
                        prix = FactureTool.CalculePrixDeleteReservatio(res, formPrix,formulesParking,personne);
                        facture.FACT_Somme = prix;
                        facture.ReservationId = res.RES_Id;
                        Facture f = await _factureService.AddFacture(facture);
                        res.FactureId = f.FACT_Id;
                        await unitOfWork.SaveAsync();
                    }
                }
                else//si reservation a debutée
                {
                    facture.ReservationId = res.RES_Id;
                   

                    if (res.RES_DureeTotal_Initiale == 0 && formType.FORM_Title == "Heure") // si c'est par Heurs Chisie ou Imposée!!!
                    {

                        prix = FactureTool.CalculeSommeHeursExacte(res.RES_DateDebut, DateTime.Now,res.RES_DateReservation, formPrix,personne);
                        facture.FACT_Somme = prix;

                        res.RES_DureeTotal_Reele = FactureTool.CalculeTotalHeurs(res.RES_DateDebut, DateTime.Now);
                        res.RES_DureeTotal_Initiale = res.RES_DureeTotal_Reele;
                        res.RES_Cloture = true;
                       
                    }
                    else // Pour tout les autres formules On calcule En Jours !!!
                    {
                        res.RES_DureeTotal_Reele = ReservationTool.CalculeTempsReel(res.RES_DateDebut, DateTime.Now);
                        decimal difference = ReservationTool.CalculeDifferenceInit_Reel(res.RES_DureeTotal_Initiale, res.RES_DureeTotal_Reele);

                     
                            prix = FactureTool.CalculPrix(res, difference, formPrix,formulesParking,personne);
                            facture.FACT_Somme = prix;
                            res.RES_Cloture = true;                         
                     
                        
                    }
                    //await unitOfWork.SaveAsync();
                    Facture f = await _factureService.AddFacture(facture);
                    res.FactureId = f.FACT_Id;
                    await unitOfWork.SaveAsync();
                }
                //var place = await unitOfWork.Place.GetPlaceById(res.PlaceId);
                //await unitOfWork.Place.UpdatePlaceParkinglibre(place, res);
                
                await unitOfWork.Reservation.UpdateResCloture(res);
               // await unitOfWork.SaveAsync();
                return res;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReadResClientBo> UpdateReservationClient(UpdateClientResBO updateClientRes)
        {
            try
            {
                ReadResClientBo r = new ReadResClientBo();
                var client = await unitOfWork.Client.GetClientById(updateClientRes.ClientId);
                var reservation = await unitOfWork.Reservation.GetReservationById(updateClientRes.RES_Id);
               // if (reservation == null) throw new Exception();//todo mettre du contenu
                if (reservation.ClientId != client.Cli_Id) { throw new ClientReservationExeption(); }
                if (reservation == null) { throw new ReservationNotFoundExeption(updateClientRes.RES_Id); }
                if (reservation.ClientId != updateClientRes.ClientId) { throw new ArgumentException("La reservation n'est pas au client " + updateClientRes.ClientId + " !!."); }
                else
                {
                    if (reservation.RES_DateDebut > reservation.RES_DateReservation)//si avant debut de reservation
                    {
                        if (/*updateClientRes.PlaceId !=0 &&*/ updateClientRes.PlaceId != reservation.PlaceId) // si la on veux changer de place avant debut 
                        {
                            //var reponce = await unitOfWork.Reservation.ReservationsDunePlace((int)updateClientRes.PlaceId, reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);
                            //if (reponce == false) { throw new ArgumentException("La Place que vous desirez n'est pas disponible pour les dates choisies !!"); }
                            //else 
                            //{
                            //    reservation.PlaceId = (int)updateClientRes.PlaceId;
                            //}
                            reservation.PlaceId = (int)updateClientRes.PlaceId;
                        }
                        else if (reservation.RES_DateFin != null || reservation.RES_DateFin < updateClientRes.RES_DateFin)
                        {
                            reservation.RES_DateFin = updateClientRes.RES_DateFin;
                            reservation.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);
                        }
                        else if(updateClientRes.FormPrixId != 0 && reservation.FormPrixId != updateClientRes.FormPrixId)
                        {
                            var formDepart = await unitOfWork.Formule.GetFormuleById(reservation.FormPrixId);
                            var formNouvelle = await unitOfWork.Formule.GetFormuleById((int)updateClientRes.FormPrixId);
                            if(formDepart.FORM_Prix < formNouvelle.FORM_Prix)
                            {
                                reservation.FormPrixId = formNouvelle.FORM_Id;
                                var fPrix = await unitOfWork.Formule.GetFormuleByIdWithType((int)reservation.FormPrixId);//recupere formPrix et nomType de la formule
                                var resv = ReservationTool.CalculeDateFin(reservation, fPrix);
                                reservation.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);//ca
                            }
                            else { throw new FormulePrixReservationExeption(); }
                           
                        }
                        var rs = await unitOfWork.Reservation.GetReservationsUpdateClient(await unitOfWork.Reservation.UpdateReservationClient(reservation));
                        r = mapper2.Map<ReadResClientBo>(rs);
                        await unitOfWork.SaveAsync();
                        //var res = await unitOfWork.Reservation.UpdateReservationClient(reservation);
                        
                        return r;
                    }
                    else //si date debut est deja commencé ou depassé
                    {

                        if (reservation.RES_DateFin != null || reservation.RES_DateFin < updateClientRes.RES_DateFin)
                        {
                            reservation.RES_DateFin = updateClientRes.RES_DateFin;
                            reservation.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);
                        }
                        else if (updateClientRes.FormPrixId != 0 && reservation.FormPrixId != updateClientRes.FormPrixId)
                        {

                            var formDepart = await unitOfWork.Formule.GetFormuleById(reservation.FormPrixId);
                            var formNouvelle = await unitOfWork.Formule.GetFormuleById((int)updateClientRes.FormPrixId);

                            if (formDepart.FORM_Prix == 0 ||formDepart.FORM_Prix < formNouvelle.FORM_Prix)
                            {
                                reservation.FormPrixId = formNouvelle.FORM_Id;
                                var fPrix = await unitOfWork.Formule.GetFormuleByIdWithType((int)reservation.FormPrixId);//recupere formPrix et nomType de la formule
                                var resv = ReservationTool.CalculeDateFin(reservation, fPrix);
                                reservation.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);
                            }
                            else { throw new FormulePrixReservationExeption(); }
                        }
                        var rs = await unitOfWork.Reservation.GetReservationsUpdateClient(await unitOfWork.Reservation.UpdateReservationClient(reservation));
                        r = mapper2.Map<ReadResClientBo>(rs);//await unitOfWork.SaveAsync();


                        await unitOfWork.SaveAsync();    
                    }
                    return r;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<ReadReservationParkingBO>> GetReservationsParking(int parkingId)
        {
            try
            {
                List<ReadReservationParkingBO> lst = new List<ReadReservationParkingBO>();
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                {
                    List<Reservation> lstReserv = await unitOfWork.Reservation.GetReservationsByParking(parkingId);
                    if (lstReserv.Count > 0)
                    {
                        lst = mapper2.Map<List<ReadReservationParkingBO>>(lstReserv);

                        await unitOfWork.SaveAsync();
                    }
                    //else { throw new ArgumentException("La listde reservation du client est vide !!."); }
                }
                return lst;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<StatReservationDebFinBO> GetReservationsDebutEtFinStats(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                {
                    var rep = await unitOfWork.Reservation.GetReservationsDebutEtFin(parkingId, date);
                    return rep;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetNombreRes(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                {
                    var rep = await unitOfWork.Reservation.GetReservationNombre(parkingId, date);
                    return rep;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetNombreResMois(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                {
                    var rep = await unitOfWork.Reservation.GetReservationNombreDuMois(parkingId, date);
                    return rep;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
