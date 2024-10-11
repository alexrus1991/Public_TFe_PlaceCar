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
    public class PreferenceService : IPreferenceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public PreferenceService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        public async Task AddPreferance(AddPrefBO prefBO)
        {
            try
            {
                if(prefBO == null) { throw new ArgumentException("Le Parametre preferenceBO est vide !!."); }
                else
                {
                    var pref = mapper2.Map<Preferences>(prefBO);

                    await unitOfWork.Preferances.AddPreferance(pref);
                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<AddPrefBO> DeletePreference(AddPrefBO prefBO)
        {
            try
            {
                AddPrefBO prf= new AddPrefBO();
                if (prefBO == null) { throw new ArgumentException("Le Parametre preferenceBO est vide !!."); }
                else
                {
                    var p = mapper2.Map<Preferences>(prefBO);
                    var prefExiste = await unitOfWork.Preferances.GetPreferenceById(p);
                    if (prefExiste == null) { throw new ArgumentException("Le preference demandé n'existe pas !!.");return prefBO; }
                    else
                    {
                      var pr = await unitOfWork.Preferances.DeletePreference(prefExiste);
                      prf = mapper2.Map<AddPrefBO>(pr);
                        
                    }
                    
                    await unitOfWork.SaveAsync();
                }
                return prf;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ReadPrefBO>> GetPreferances(int parkingId, int clientId)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if(parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas!!"); }
                else
                {
                    var client = await unitOfWork.Client.GetClientById(clientId);
                    if (client == null) { throw new ArgumentException("Le client spécifié n'existe pas!!"); }
                    else
                    {
                        List<Preferences> prefList = await unitOfWork.Preferances.GetPreferances(parkingId, clientId);

                        var lisPref = mapper2.Map<List<ReadPrefBO>>(prefList);

                        return lisPref;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
