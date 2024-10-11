using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        //Task AddReservation(AddReservationBo reservationBo);
        Task<Reservation> AddReservation(Reservation reservation);
        // Task UpdateResCloture(int clientId,int resId);
        Task UpdatereservationDelete(int resId, int factureId);
        //Task UpdateResFactureId(int resId, int factureId);
        //Task UpdateResTempsReel(int clientId, int resId , decimal tempsReel);
        Task<Reservation> UpdateResCloture(Reservation reservation);
        Task<List<Reservation>> GetReservationsPourClient(int clientId, bool isClotured=false);
        Task<Reservation?> GetReservationById( int reservationId);
        Task<bool> ReservationsDunePlace(int placeId, DateTime dateDebut, DateTime dateFin);
        Task DeleteReservationClient(int reservationId ,int clientId);
        Task<Reservation> UpdateReservationClient(Reservation reservation);
        Task<Reservation> GetReservationsUpdateClient(Reservation reservation);
        Task<StatReservationDebFinBO> GetReservationsDebutEtFin(int parkingId, DateTime date);
        Task<int> GetReservationNombre(int parkingId, DateTime date);
        Task<int> GetReservationNombreDuMois(int parkingId, DateTime date);
        Task<List<Reservation>> GetReservationsByParking(int parkingId);
        Task<List<StatReservationParMois>> GetStatReservation();
    }
}

