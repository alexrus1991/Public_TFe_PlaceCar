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
    public class PaysRepository : IPaysRepository
    {
        private readonly PlaceCarDbContext _context;

        //public List<PaysEntity> lstPays = new List<PaysEntity>();

        public PaysRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddPays(PaysEntity pays)
        {
            try
            {
                if (pays != null)
                {
                    await _context.Pays.AddAsync(pays);
                }
                else { throw new ArgumentNullException(nameof(pays)); }
            }
            catch(Exception ex) 
            { throw; }
   
        }

        public async Task<List<PaysEntity>> GetAllPays()
        {
           var list = await _context.Pays
          .AsNoTracking()
          .Include(p => p.Adresses)
              .ThenInclude(a => a.Parking) 
          .ToListAsync();

            return list;
        }

        public async Task<PaysEntity> GetPaysById(int id)
        {
            var pays = await _context.Pays
            .AsNoTracking()
               .FirstOrDefaultAsync(p => p.PAYS_Id == id);
            return pays;
        }

        public async Task<bool> PaysExiste(string paysNom)
        {
            var paysExistants = await _context.Pays.FirstOrDefaultAsync(p => EF.Functions.Like(p.PAYS_Nom, paysNom));
            if (paysExistants == null) { return false; }
            return true;
        }
    }
}
