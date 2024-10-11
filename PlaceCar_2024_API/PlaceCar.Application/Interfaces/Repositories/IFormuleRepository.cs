using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IFormuleRepository
    {
        Task<FormuleDePrix> AddFurmule(FormuleDePrix formule);
        Task<FormuleDePrix> GetFormuleById(int formId);
        Task<FormuleDePrix> GetFormuleByIdWithType(int formId);
        Task<FormuleDePrix> GetFormuleByParkIdandFormuleType(int parkId);
        Task<List<FormuleDePrix>> GetFormulesPrixByParkingId(int parkId);
        Task<bool> UpdateFormule(FormuleDePrix formule);
    }
}
