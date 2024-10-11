using PlaceCar.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IClientService
    {
        //Task<string> Login(string email, string password);
       // Task AddClient(PersonneBo personne);
        Task AddEmplyeClient(int employeeId);
        Task<bool> UpdateCliInfo(UpClientBO upClient);
        Task<ReadClientBo> GetCliById(int id);
        Task AddReservationClient(AddResBO reservation);
        Task AddCompteClient(AddCompteBo compteBo);
        Task<List<ReadResClientBo>> GetReservationsClient(int clientId,bool isClotured = false);
        Task<ReadResClientBo> UpdateReservationCloturer(int clientId, int reservationId);
        Task<int> GetNombreClients();
    }
}
