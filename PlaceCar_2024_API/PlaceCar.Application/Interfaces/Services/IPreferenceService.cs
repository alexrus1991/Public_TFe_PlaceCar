using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IPreferenceService
    {
        Task AddPreferance(AddPrefBO prefBO);
        Task<List<ReadPrefBO>> GetPreferances(int parkingId,int clientId);

        Task<AddPrefBO> DeletePreference(AddPrefBO prefBO);
    }
}
