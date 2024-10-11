using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IPreferancesRepository
    {
        Task AddPreferance(Preferences preference);
        Task<List<Preferences>> GetPreferances(int parkingId, int clientId);
        Task<Preferences> GetPreferenceById(Preferences preference);
        Task<Preferences> DeletePreference(Preferences preference);
        Task<bool> UpdatePlacePreference(Preferences preference,int newPlaceId);
    }
}
