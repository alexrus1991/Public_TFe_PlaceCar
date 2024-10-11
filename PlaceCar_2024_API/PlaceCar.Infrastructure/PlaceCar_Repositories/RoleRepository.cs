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
    public class RoleRepository : IRoleRepository
    {
        private readonly PlaceCarDbContext _context;

        public RoleRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Role
            .AsNoTracking()
               .FirstOrDefaultAsync(p => p.Role_Id == id);
            return role;
        }

        public async Task<List<Role>> GetRoles()
        {
            var list = await _context.Role
           .AsNoTracking()        
           .ToListAsync();

            return list;
        }
    }
}
