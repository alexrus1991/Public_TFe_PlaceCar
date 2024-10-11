using PlaceCar.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IFormulePrixService
    {
        Task<List<ReadForulesParkingBO>> GetFormulesParkingId(int parkinId);
        Task<List<ReadFormuleOptionBO>> CalculePrixsAsync(int parkingId, DateTime dateDeb, DateTime? dateFin);

        Task<bool> UpdateFormulePrix(int formuleId, decimal prix, int parkingId);
    }
}
