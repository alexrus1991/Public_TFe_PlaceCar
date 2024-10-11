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
    public class CompteRepository : ICompteRepository
    {
        private readonly PlaceCarDbContext _context;

        public CompteRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        
        public async Task AddCompteBankForClient(CompteBank compteBank)
        {
           
            try
            {
                if (compteBank != null)
                {
                    _context.Add(compteBank);
                }
                else { }
            }
            catch(Exception ex) { throw; }
            
        }

        public async Task<CompteBank> GetCompteById(int compteId)
        {
            var compte = await _context.CompteBank
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.CB_Id == compteId);
            return compte;
        }

        public async Task<CompteBank> GetCompteClient(int clientId)
        {
            var compteClient = await _context.CompteBank
                .AsNoTracking()
                .Include(c => c.Client) 
                .ThenInclude(cl => cl.Cli) 
                .FirstOrDefaultAsync(c => c.ClientId == clientId); 

            return compteClient;

        }
    }
}
