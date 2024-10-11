using PlaceCar.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IFormuleTypeService
    {
        Task AddTypeFormule(AddTypeFormBO typeFormBO);
        Task<List<ReadTypeBO>> GetTypeForm();
        Task<bool> UpdateTypeDescription(int typeId, string description);
        Task<bool> UpdateTypeTitre(int typeId, string titre);
        //Task<List<ReadTypeBO>> GetCombTypeForm(DateTime dateDeb, DateTime dateFin);
    }
}
