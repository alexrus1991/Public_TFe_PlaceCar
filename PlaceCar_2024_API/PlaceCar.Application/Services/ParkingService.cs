using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Application.MapperProfiles;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.Exceptions.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public ParkingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            mapper2 = mapper;
        }

        public async Task AddFormPrix(AddFormuleBO formuleBO)
        {
            try
            {
                if(formuleBO == null) { throw new ArgumentException("La formuleBO est vide !!"); }
                else
                {
                    var typyForm = await unitOfWork.FormuleType.GetFormTypeById(formuleBO.TypeId); 
                    if(typyForm == null) { throw new ArgumentException("La formule type spécifié n'existe pas !!"); }
                    else
                    {
                        var parking = await unitOfWork.Parking.GetParkingById(formuleBO.ParkingId);
                        if (parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas !!"); }
                        else
                        {
                            var formule = mapper2.Map<FormuleDePrix>(formuleBO);

                            FormuleDePrix f = await unitOfWork.Formule.AddFurmule(formule);
                            parking.Formules.Add(f);
                            var p = await unitOfWork.Parking.UpdateParking(parking);
                            await unitOfWork.SaveAsync();
                        }
                    }
                    
                }             
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task AddParking(ParkingBO parkingBO)
        {
            try
            {
                if(parkingBO == null) { throw new ArgumentException("Le parkingBO est vide !!"); }
                else
                {
                    var adresse = mapper2.Map<Adresse>(parkingBO);
                    var parking = mapper2.Map<ParkingEntity>(parkingBO);

                    await unitOfWork.Adresse.AddAdresse(adresse);
                    parking.Adresse = adresse;
                    await unitOfWork.Parking.AddParking(parking);

                    int NbPlaceTotal = parking.PARK_NbPlaces;
                    int NbEtages = parking.PARK_NbEtages;
                    int NbPlaceParEtage = NbPlaceTotal / NbEtages;

                    for (int etage = 1; etage <= NbEtages; etage++)
                    {
                        for (int place = NbPlaceParEtage; place > 0; place--)
                        {
                            //await AddPlaceParking(etage, place, true, parking.PARK_Id);
                            var placeParking = new PlaceParking
                            {
                                PLA_Etage = etage,
                                PLA_NumeroPlace = place,
                                Parking = parking
                            };
                           await unitOfWork.Place.AddPlaceParking(placeParking);
                        }
                    }
                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task<List<ReadParkVilleBO>> GetParkingsByPaysByVille(int paysId, string nomVille)
        {
            try
            {
                var pays = await unitOfWork.Pays.GetPaysById(paysId);
                if (pays == null) { throw new ArgumentNullException("Pays n'existe pas !!"); }
                else
                {
                    List<ParkingEntity> lst = await unitOfWork.Parking.GetParkingsByPaysEtVille(paysId,nomVille);
                    var lstmap = mapper2.Map<List<ReadParkVilleBO>>(lst);
                    return lstmap;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ReadParkVilleBO>> GetParkingsByPays(int paysId)
        {
            try
            {
                var pays = await unitOfWork.Pays.GetPaysById(paysId);
                if (pays == null) { throw new ArgumentNullException("Pays n'existe pas !!"); }
                else
                {
                    List<ParkingEntity> lst = await unitOfWork.Parking.GetParkingsParPays(paysId);
                    var lstmap = mapper2.Map<List<ReadParkVilleBO>>(lst);
                    return lstmap;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<ReadPlacesLibreBO>> GetPlacesLibresInParking(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ArgumentNullException("Parking n'existe pas !!"); }
                else
                {
                    List<PlaceParking> lesplacesLibres = await unitOfWork.Parking.GetPlacesLibresPourParking(parkingId, date);
                    var list = mapper2.Map<List<ReadPlacesLibreBO>>(lesplacesLibres);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<string>> GetVillesByPays(int paysId)
        {
            try
            {
                var pays = await unitOfWork.Pays.GetPaysById(paysId);
                if (pays == null) { throw new ArgumentNullException("Pays n'existe pas !!"); }
                List<string> lst = await unitOfWork.Parking.GetVillesParPays(paysId);
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ParkingEmpWorkBo> GetParkingEmployee(int employeeId)
        {
            try
            {
                var employee = await unitOfWork.Employe.GetEmployeeById(employeeId);
                if (employee == null) { throw new ArgumentNullException("Emloyee n'existe pas !!"); }
                var employeeWorkOn = await unitOfWork.EmpWorkOn.GetEmployeWorkOn(employeeId);
                if (employeeWorkOn == null) { throw new ArgumentNullException("Emloyee ne travaille pas dans l'entreprise PlaceCar !!"); }
                if(employee.EmpPers.PERS_Id != employeeWorkOn.Emp_Pers_Id) { throw new ArgumentNullException("Emloyee ne travaille pas dans l'entreprise PlaceCar !!"); }
                else
                {
                    var parking = await unitOfWork.Parking.GetParkingById(employeeWorkOn.ParkingId);
                    var parkInfo = mapper2.Map<ParkingEmpWorkBo>(parking);
                    return parkInfo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ReadAllParkingsBO>> GetParkingsAll()
        {
            try
            {
                List<ParkingEntity> lst = await unitOfWork.Parking.GetParkings();
                var lstmap = mapper2.Map<List<ReadAllParkingsBO>>(lst);
                return lstmap;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReadParkingBO> GetParkingById(int id)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(id);
                if (parking == null) { throw new ParkingNotFoundExeption(id); }
                else 
                { 
                    var prk = mapper2.Map<ReadParkingBO>(parking);
                    return prk;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
