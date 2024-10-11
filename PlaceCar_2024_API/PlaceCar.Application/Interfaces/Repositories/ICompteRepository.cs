using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface ICompteRepository
    {
        Task AddCompteBankForClient(CompteBank compte);
        Task<CompteBank> GetCompteById(int compteId);
        Task<CompteBank> GetCompteClient(int clientId);
    }
}
