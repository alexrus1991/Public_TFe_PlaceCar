using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class PersonneRoleRepository : IPersonneRoleRepository
    {
        private readonly PlaceCarDbContext _context;

        public PersonneRoleRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<PersonneRole> GetPersonneRole(int userId,int roleId)
        {
            var pr = await _context.PersonneRole
               .AsNoTracking()
               .Include(p=>p.Personne)
               .Include(p=>p.Role)
               .FirstOrDefaultAsync(p => p.PersonneId == userId && p.RoleId == roleId );
            return pr;
        }

        public async Task AddPersonneRole(PersonneRole personneRole)
        {
            try
            {
                if (personneRole != null)
                {
                    _context.PersonneRole.Add(personneRole);
                   // _context.Entry<Role>(personneRole.Role).State = EntityState.Unchanged;
                }
                else { throw new ArgumentNullException(nameof(personneRole)); }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
