using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> UpdateClotureReservationClient(int resId,int clientId);
        Task DeleteReservation(int resId,int clientId);
        Task<ReadResClientBo> UpdateReservationClient(UpdateClientResBO updateClientRes);
        Task<List<ReadReservationParkingBO>> GetReservationsParking(int parkingId);
        Task<StatReservationDebFinBO> GetReservationsDebutEtFinStats(int parkingId, DateTime date);
        Task<int> GetNombreRes(int parkingId, DateTime date);
        Task<int> GetNombreResMois(int parkingId, DateTime date);
    }
}
