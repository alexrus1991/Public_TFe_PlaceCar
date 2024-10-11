using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task AddClient(Client client);
        //Task<bool> UpdateClientInfo(int id, string pwd);
        Task<Client> GetClientById(int clientId);
        Task<int> GetClientNombreTotal();
        Task<Client> GetClientPersonneById(int personneId);
    }
}
