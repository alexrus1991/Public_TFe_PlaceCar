using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IInfoEntrpriseRepository
    {
        Task<InfoEntreprise> GetInfoEntreprise(string numCompte);
        Task<InfoEntreprise> GetInfoEntreprise();
    }
}
