using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRoles();
        Task<Role> GetRoleById(int id);
    }
}
