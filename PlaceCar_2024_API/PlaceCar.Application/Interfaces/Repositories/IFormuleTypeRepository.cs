using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IFormuleTypeRepository
    {
        Task AddFormType(FormuleDePrixType formulType);
        Task<List<FormuleDePrixType>> GetAllTypeFormules();

        Task<FormuleDePrixType> GetFormTypeById(int typeId);
        Task<bool> UpdateFormuleTypeDescription(int id, string description);
        Task<bool> UpdateFormuleTypeTitre(int id, string titre);
    }
}
