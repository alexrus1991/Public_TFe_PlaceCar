using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.BusinessTools;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlaceCar.Application.Services
{
    public class FormulePrixService : IFormulePrixService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public FormulePrixService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }


        public async Task<List<ReadForulesParkingBO>> GetFormulesParkingId(int parkinId)
        {
            try
            {
                List<ReadForulesParkingBO> lst = new List<ReadForulesParkingBO>();
                var parking = await unitOfWork.Parking.GetParkingById(parkinId);
                if(parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas!!"); }
                else
                {
                    var formules = await unitOfWork.Formule.GetFormulesPrixByParkingId(parking.PARK_Id);
                    lst = mapper2.Map<List<ReadForulesParkingBO>>(formules);
                    await unitOfWork.SaveAsync();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateFormulePrix(int formuleId, decimal prix, int parkingId)
        {
            try
            {
                var formulePrix = await unitOfWork.Formule.GetFormuleById(formuleId);
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ArgumentException("Le parking demandé est vide !!"); }
                //if (!parking.Formules.Contains(formulePrix) && formulePrix != null) { throw new ArgumentException("La Formule n'appartien pas au parking en question !!"); }
                if (formulePrix.ParkingId != parking.PARK_Id && formulePrix != null) { throw new ArgumentException("La Formule n'appartien pas au parking en question !!"); }
                else
                {
                    formulePrix.FORM_Prix = prix;
                    bool rep = await unitOfWork.Formule.UpdateFormule(formulePrix);                
                    await unitOfWork.SaveAsync();
                    return rep;
                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ReadFormuleOptionBO>> CalculePrixsAsync(int parkingId, DateTime dateDeb, DateTime? dateFin)
        {
            try
            {
                // DateTime DateFinReelle = dateFin.HasValue ? dateFin.Value : dateDeb;
                FormuleDePrix formule = null;
                var listReponce = new List<ReadFormuleOptionBO>();

                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas!!"); }
                else
                {
                    var formules = await unitOfWork.Formule.GetFormulesPrixByParkingId(parking.PARK_Id);
                    if (formules == null) { throw new ArgumentException("Aucune formule de prix n'a était trouvée pour ce parking"); }
                    else
                    {
                        listReponce = FactureTool.CalculePrixAdaptee(formules, dateDeb, dateFin,formule);
                        if (listReponce == null) { throw new ArgumentException("Aucune proposition de prix peut etre proposée"); }
                    }

                    return listReponce;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
