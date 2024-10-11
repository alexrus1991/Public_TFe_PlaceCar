using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IPersonneRepository
    {
        Task AddPersonne(Personne personne,Role role);
        Task<bool> UpdatePersonneInfo(int id, string pwd);
        Task<Personne> GetPersonneById(int clientId);
        Task<Personne> GetPersonneByEmail(string email);
        Task DeletePersonne(int personId);
    }
}
