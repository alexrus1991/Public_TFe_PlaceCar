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
    public class FormuleTypeService : IFormuleTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public FormuleTypeService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }
 

        public async Task AddTypeFormule(AddTypeFormBO typeFormBO)
        {
            try
            {
                if (typeFormBO == null) { throw new ArgumentException("Le model FormuleTypeBO est Vide !!"); }
                else
                {

                    var formType = mapper2.Map<FormuleDePrixType>(typeFormBO);

                    await unitOfWork.FormuleType.AddFormType(formType);

                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<ReadTypeBO>> GetTypeForm()
        {
            try
            {
                List<ReadTypeBO> listBo = new List<ReadTypeBO>();
                List<FormuleDePrixType> typeList = await unitOfWork.FormuleType.GetAllTypeFormules();
                if(typeList == null) { throw new ArgumentException("La List de FormuleType est Vide !!"); }
                else
                {
                    listBo = mapper2.Map<List<ReadTypeBO>>(typeList);
                    await unitOfWork.SaveAsync();
                }
                return listBo;
            }
            catch (Exception ex )
            {

                throw;
            }
   
        }
       

        public async Task<bool> UpdateTypeDescription(int typeId, string description)
        {
            bool Ok = false;
            try
            {
                var type = await unitOfWork.FormuleType.GetFormTypeById(typeId);
                if (type == null) { throw new ArgumentException("La FormuleType Id est Vide !!"); }
                else
                {
                   bool rep = await unitOfWork.FormuleType.UpdateFormuleTypeDescription(typeId, description);
                   if (!rep) {  Ok = rep; } 
                    else { Ok = rep; }
                }
                return Ok;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateTypeTitre(int typeId, string titre)
        {
            bool Ok = false;
            try
            {
                var type = await unitOfWork.FormuleType.GetFormTypeById(typeId);
                if (type == null) { throw new ArgumentException("La FormuleType Id est Vide !!"); }
                else
                {
                    bool rep = await unitOfWork.FormuleType.UpdateFormuleTypeTitre(typeId, titre);
                    if (!rep) { Ok = rep; }
                    else { Ok = rep; }
                }
                return Ok;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
