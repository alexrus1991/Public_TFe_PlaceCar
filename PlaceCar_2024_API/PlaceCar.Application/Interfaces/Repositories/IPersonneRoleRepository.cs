using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IPersonneRoleRepository
    {
        Task AddPersonneRole(PersonneRole personneRole);
        Task<PersonneRole> GetPersonneRole(int userId,int roleId);
    }
}
