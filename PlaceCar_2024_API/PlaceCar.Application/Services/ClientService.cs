using AutoMapper;
using PlaceCar.Application.Interfaces.Provider;
using PlaceCar.Application.Interfaces.PwdHasher;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.BusinessTools;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using PlaceCar.Domain.Exceptions.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IReservationService _reservationService;
        //private readonly IFactureService _factureService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper2,IReservationService reservationService, IPasswordHasher passwordHasher,IJwtProvider jwtProvider)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
            this._reservationService = reservationService;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        //public async Task<string> Login(string email, string password)
        //{
        //    try
        //    {
        //        var personne = await unitOfWork.Personne.GetPersonneByEmail(email);

        //        var result = _passwordHasher.Verify(password, personne.PERS_Password);

        //        if(result == false) { throw new Exception("Le loginn'est pas correcte !!"); }

        //        var token = _jwtProvider.CreateToken(personne);

        //        return token;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //public async Task AddClient(PersonneBo personne)
        //{
        //    try
        //    {
        //        if(personne == null) { throw new ArgumentException("Le Parametre personne est vide !!."); }
        //        else
        //        {
        //            var personn = mapper2.Map<Personne>(personne);
        //            personn.PERS_Password = _passwordHasher.Generate(personn.PERS_Password);
        //            var client = new Client{ Cli = personn };

        //            await unitOfWork.Personne.AddPersonne(personn);
        //            await unitOfWork.Client.AddClient(client);
        //            await unitOfWork.SaveAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
            
        //}

        //public async Task AddReservationClient(AddResBO reservation)
        //{
        //    try
        //    {             
        //        if(reservation == null) { throw new ArgumentException("La reservationBO est vide !!"); }
        //        else
        //        {
        //            var client = await unitOfWork.Client.GetClientById(reservation.ClientId);
        //            if (client == null) { throw new ArgumentException("Le client spécifié n'existe pas !!"); }
        //            else
        //            {

        //                bool reponce = await unitOfWork.Reservation.ReservationsDunePlace(reservation.PlaceId, reservation.RES_DateDebut, reservation.RES_DateFin);
        //                if (reponce == false) { throw new ArgumentException("La Place que vous desiréz n'est pas disponible pour les dates choisies !! !!"); }
        //                else
        //                {
        //                    var res = mapper2.Map<Reservation>(reservation);

        //                    var fPrix = await unitOfWork.Formule.GetFormuleByIdWithType(reservation.FormPrixId);//recupere formPrix et nomType de la formule
        //                    var resv = ReservationTool.CalculeDateFin(res, fPrix);//calcule date fin si elle est null mais formule de prix est choisie
        //                    if(resv.RES_DateFin == null)//si datefin est null quand-mëme,alors on met forprix de type heure par defaut et datefin reste null
        //                    {
        //                        var place = await unitOfWork.Place.GetPlaceById(reservation.PlaceId);
        //                        var formpris = await unitOfWork.Formule.GetFormuleByParkIdandFormuleType(place.ParkingId);
        //                        res.FormPrixId = formpris.FORM_Id;
        //                    }
        //                    else
        //                    {
        //                         res.RES_DateFin = resv.RES_DateFin;
        //                    res.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)resv.RES_DateFin);
        //                    }


        //                    var r = await unitOfWork.Reservation.AddReservation(res);
        //                    //Facture f = new Facture() { FACT_Somme = fPrix.FORM_Prix, ReservationId = r.RES_Id };
        //                    //await unitOfWork.Facture.AddFacture(f);


        //                    await unitOfWork.SaveAsync();
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        public async Task AddReservationClient(AddResBO reservation)
        {
            try
            {
                if (reservation == null) { throw new ArgumentException("La reservationBO est vide !!"); }
                else
                {
                    var client = await unitOfWork.Client.GetClientById(reservation.ClientId);
                    var facures = await unitOfWork.Facture.GetAllFacturesClient(client.Cli_Id);

                    bool nonPaye = FactureTool.FacturesNonPayé(facures);    //si il y a une facture pas reglé , alor pas de reservation
                    if(nonPaye) { throw new FactureNotPaidExeption(); }
                    if (client == null) { throw new ClientNotFoundExeption(); }
                    else
                    {
                        if (reservation.PlaceId == 0) { throw new PlacePakingExeption(); }
                        else 
                        {
                            var place = await unitOfWork.Place.GetPlaceById(reservation.PlaceId);
                            var res = mapper2.Map<Reservation>(reservation);


                            if(res.RES_DateFin != null && res.FormPrixId != 0)
                            {
                                //Datedebut = 15/08/2024 9:00:00
                                //Datefin = 01/01/01 00:00:00 ==> Angular doit t'envoyer 15/08/2024 18:00:00
                                

                                res.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)reservation.RES_DateFin);
                            }
                            //else if(res.RES_DateFin == null && res.FormPrixId != 0) // 
                            //{
                            //    //1- Datefin ==> null
                            //    //2- choisir la formule heure

                            //    var fPrix = await unitOfWork.Formule.GetFormuleByIdWithType((int)reservation.FormPrixId);//recupere formPrix et nomType de la formule
                            //    var resv = ReservationTool.CalculeDateFin(res, fPrix);//calcule date fin si elle est null mais formule de prix est choisie
                            //    res.RES_DateFin = resv.RES_DateFin;
                            //    res.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)res.RES_DateFin);
                            //}
                            else if (res.RES_DateFin == null && res.FormPrixId == 0)
                            {
                                //var place = await unitOfWork.Place.GetPlaceById(reservation.PlaceId);
                                var formpris = await unitOfWork.Formule.GetFormuleByParkIdandFormuleType(place.ParkingId);
                                res.FormPrixId = formpris.FORM_Id;
                                res.RES_DureeTotal_Initiale = 0;

                            }
                            else if (res.RES_DateFin != null &&  res.FormPrixId == 0)
                            {
                                var formpris = await unitOfWork.Formule.GetFormuleByParkIdandFormuleType(place.ParkingId);
                                res.FormPrixId = formpris.FORM_Id;
                                res.RES_DureeTotal_Initiale = ReservationTool.CalculeTempsInitiale(reservation.RES_DateDebut, (DateTime)res.RES_DateFin);
                            }   
                            //await unitOfWork.Place.UpdatePlaceParking(place,res);
                            await unitOfWork.SaveAsync();
                            var r = await unitOfWork.Reservation.AddReservation(res);
                        }
                    }                  
                }
               
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ReadClientBo> GetCliById(int id)
        {
           var client = await unitOfWork.Client.GetClientById(id);
            if (client != null) 
            {
                ReadClientBo clientBo = mapper2.Map<ReadClientBo>(client);
                return clientBo;
            }
            else { throw new Exception(); }
        }

        public async Task<bool> UpdateCliInfo(UpClientBO upClient)
        {
            try
            {
                if (upClient == null) return false;
                var client = await unitOfWork.Client.GetClientById(upClient.Id);
                if (client == null) { throw new ClientNotFoundExeption(); }
                var paersonne = await unitOfWork.Personne.GetPersonneById(client.Cli_Id);
                if (paersonne.PERS_Id != client.Cli.PERS_Id) { throw new ArgumentException("Le personne spécifié n'est pas le client demandé !!"); }
                else
                {
                   string newPWD = _passwordHasher.Generate(upClient.pwd);
                   await unitOfWork.Personne.UpdatePersonneInfo(paersonne.PERS_Id, newPWD);
                   return true;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            //if(upClient == null) return false;
            //else
            //{
            //    var infoPersonne = mapper2.Map<Personne>(upClient);

            //    await unitOfWork.Personne.UpdatePersonneInfo(infoPersonne.PERS_Id,  infoPersonne.PERS_Password);
            //    return true;
            //}
            
        }
        
        public async Task AddCompteClient(AddCompteBo compteBo)
        {
            if (compteBo == null) { throw new ArgumentException("Le comtenant compteBO spécifié est vide!!"); }
            else
            {
                var client = await unitOfWork.Client.GetClientById(compteBo.ClientId);
                if(client == null) { throw new ArgumentException("Le client spécifié n'existe pas"); }
                else
                {
                    var cmp = mapper2.Map<CompteBank>(compteBo);
                    cmp.Client = client;
                    cmp.CB_Date = DateTime.Now;
                    cmp.CB_NumCompte = CompteBankTool.GenereUniqueNumCompte();

                    await unitOfWork.Compte.AddCompteBankForClient(cmp);

                    await unitOfWork.SaveAsync();
                }
            }
        }

        public async Task<List<ReadResClientBo>> GetReservationsClient(int clientId, bool isClotured =false)
        {     
            try
            {
                List<ReadResClientBo> lst = new List<ReadResClientBo>();
                var client = await unitOfWork.Client.GetClientById(clientId);
                if(client == null) { throw new ClientNotFoundExeption(); }
                {
                    List<Reservation> lstReserv = await unitOfWork.Reservation.GetReservationsPourClient(clientId, isClotured);
                    if(lstReserv.Count > 0)
                    {
                        lst = mapper2.Map<List<ReadResClientBo>>(lstReserv);
                        
                        await unitOfWork.SaveAsync();
                    }
                    //else { throw new ArgumentException("La list de reservation du client est vide !!."); }
                }
                return lst;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReadResClientBo> UpdateReservationCloturer(int clientId, int reservationId)
        {
            try
            {
                List<ReadResClientBo> lst = new List<ReadResClientBo>();
                var client =  await unitOfWork.Client.GetClientById(clientId);
                if (client == null) { throw new ClientNotFoundExeption(); }
                else
                {
                   Reservation r = await _reservationService.UpdateClotureReservationClient(reservationId,client.Cli_Id);
                   
                    if(r != null)
                    {
                        var lesRescloturees = await unitOfWork.Reservation.GetReservationsPourClient(client.Cli_Id, true);
                        lst = mapper2.Map<List<ReadResClientBo>>(lesRescloturees);

                        ReadResClientBo reponce = lst.FirstOrDefault(res => res.RES_Id == r.RES_Id);

                        await unitOfWork.SaveAsync();
                        return reponce;
                    }

                    return new ReadResClientBo();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AddEmplyeClient(int employeeId)
        {
            try
            {
                var employee = await unitOfWork.Employe.GetEmployeeById(employeeId);
                if (employee == null) { throw new EmployeeNotFoundExeption(employeeId); }
                {
                    var client = new Client { Cli = employee.EmpPers, Cli_Id = employee.EmpPers.PERS_Id };//
                    await unitOfWork.Client.AddClient(client);

                    var role = await unitOfWork.Role.GetRoleById((await unitOfWork.Role.GetRoles()).First(r => r.Role_Name == RoleEnum.Client.ToString()).Role_Id);

                    await unitOfWork.PersonneRole.AddPersonneRole(new PersonneRole
                    {
                        PersonneId = employee.Emp_Pers_Id,
                        RoleId = role.Role_Id,
                    });                
                }
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> GetNombreClients()
        {
            return await unitOfWork.Client.GetClientNombreTotal();
        }
    }
}
