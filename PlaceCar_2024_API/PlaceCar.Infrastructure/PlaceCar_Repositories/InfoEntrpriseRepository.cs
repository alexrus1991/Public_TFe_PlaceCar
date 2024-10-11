using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class InfoEntrpriseRepository : IInfoEntrpriseRepository
    {
        private readonly PlaceCarDbContext _context;

        public InfoEntrpriseRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<InfoEntreprise> GetInfoEntreprise(string numCompte)
        {
            try
            {
                var info = await _context.InfoEntreprise
                    .AsNoTracking()
                    .Where(r => r.Cb_NumCompte == numCompte)
                    .FirstOrDefaultAsync();

                return info;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<InfoEntreprise> GetInfoEntreprise()
        {
            try
            {
                var info = await _context.InfoEntreprise
                    .AsNoTracking()             
                    .FirstOrDefaultAsync();

                return info;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
