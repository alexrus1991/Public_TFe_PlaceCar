using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class PreferancesRepository : IPreferancesRepository
    {
        private readonly PlaceCarDbContext _context;

        public PreferancesRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddPreferance(Preferences preference)
        {
            try
            {
                if (preference == null) { throw new ArgumentException("L'Entité Preference spécifié est vide !!!"); }
                else
                {
                    _context.Preference.Add(preference);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Preferences> DeletePreference(Preferences preference)
        {
            try
            {
                if(preference == null) { throw new ArgumentException("L'Entité Preference spécifié est vide !!!"); }
                else
                {
                    var pref = _context.Preference
                    .FirstOrDefault(p => p.PlaceId == preference.PlaceId && p.ParkingId == preference.ParkingId && p.ClientId == preference.ClientId);

                    if (pref != null)
                    {
                        _context.Preference.Remove(pref);
                    }
                    return preference;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Preferences>> GetPreferances(int parkingId, int clientId)
        {
            var listePref = await _context.Preference
                .AsNoTracking()
                .Include(p => p.Parking)
                .Include(p=>p.Place)
                .Include(p=>p.Client)
                .ThenInclude(c => c.Cli)
                .Where(p=> p.ClientId == clientId && p.ParkingId == parkingId)
                .ToListAsync();

            return listePref;
        }

        public async Task<Preferences> GetPreferenceById(Preferences preference)
        {
            var pref = await _context.Preference
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.PlaceId == preference.PlaceId && p.ParkingId == preference.ParkingId && p.ClientId == preference.ClientId);
            return pref;
        }

        public async Task<bool> UpdatePlacePreference(Preferences preference, int newPlaceId)
        {
            var existingPreference = await _context.Preference
            .FirstOrDefaultAsync(p => p.PlaceId == preference.PlaceId &&
                                      p.ParkingId == preference.ParkingId &&
                                      p.ClientId == preference.ClientId);

            if (existingPreference != null)
            {
                existingPreference.PlaceId = newPlaceId;
                return true;
            }
            else { return false; }
        }
    }
}
