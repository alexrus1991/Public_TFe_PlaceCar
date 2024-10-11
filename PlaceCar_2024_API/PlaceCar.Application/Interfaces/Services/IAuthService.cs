using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task Logout(string token);
        Task AddEmplyee(AddEmpBO empBO, bool IsAdmin = false);
        Task AddClient(PersonneBo personne);
        Task<List<Role>> GetRoles();
    }
}
