using AutoMapper;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class PaysService : IPaysService
    {
      
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;
        public PaysService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        //public async Task AddNewPays(string nomPays)  
        //{

        //    try
        //    {
        //        bool ok = await unitOfWork.Pays.PaysExiste(nomPays);
        //        if (nomPays == null) { throw new InvalidOperationException("Le Nom du paays est vide !!!"); }
        //        else if (ok == true) { throw new InvalidOperationException("Le Pays Existe déjà dans la base de donnée"); }
        //        else
        //        {
        //            var pays = new PaysEntity { PAYS_Nom = nomPays };
        //            await unitOfWork.Pays.AddPays(pays);
        //            await unitOfWork.SaveAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<List<PaysBO>> GetAllPays()
        {
            try
            {
                List<PaysBO> result = new List<PaysBO> ();
                List<PaysEntity> mesPays = await unitOfWork.Pays.GetAllPays();
                if (mesPays == null && mesPays.Count > 0) { throw new ArgumentException("La liste de pays est vide !!"); }
                else
                {
                    result = mapper2.Map<List<PaysBO>>(mesPays);
                 
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           // List<PaysEntity> mesPays = await unitOfWork.Pays.GetAllPays();
            //List<PaysBO> result = mesPays.Select(p => new PaysBO() 
            //{ PAYS_Nom = p.PAYS_Nom,
            //    Adresse = p.Adresses.FirstOrDefault()?.ADRS_NomRue 
            //}).ToList();
            ////mapper
            

             
        }
    }
}
