using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly PlaceCarDbContext _context;

        public ReservationRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            try
            {
                if (reservation == null) { throw new ArgumentException("La résèrvation est vide !!!"); }
                else
                {
                    var res = new Reservation
                    {
                        RES_DateReservation = DateTime.Now,
                        RES_DateDebut = reservation.RES_DateDebut,
                        RES_DateFin = reservation.RES_DateFin,
                        RES_DureeTotal_Initiale = reservation.RES_DureeTotal_Initiale,
                        RES_DureeTotal_Reele = 0,
                        RES_Cloture = false,
                        ClientId = reservation.ClientId,
                        PlaceId = reservation.PlaceId,
                        FactureId = reservation.FactureId,
                        FormPrixId = reservation.FormPrixId
                        
                    };
                    _context.Reservation.Add(res);
                    await _context.SaveChangesAsync();
                    return res;
                }               
            }
            catch(Exception ex)  
            { 
                throw; 
            }
           
        }

        public async Task DeleteReservationClient(int reservationId, int clientId)
        {
            try
            {
                var reservation = await _context.Reservation
                    .Where(r => r.RES_Id == reservationId && r.ClientId == clientId)
                    .FirstOrDefaultAsync();

                if (reservation == null)
                {
                    throw new ArgumentException("La réservation spécifiée n'existe pas pour ce client.!!!");
                }

                _context.Reservation.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Reservation?> GetReservationById( int reservationId)
        {
            var reservation = await _context.Reservation
                    .AsNoTracking()
                    .Where(r =>  r.RES_Id == reservationId)
                    .FirstOrDefaultAsync();

            return reservation;
        }


        // GetReservationsPourClient (1) ==> celle non cloturé pour le client 1
        // GetReservationsPourClient (1, false) ==> celle non cloturé pour le client 1
        // GetReservationsPourClient (1, true) ==> celle  cloturé pour le client 1

        public async Task<List<Reservation>> GetReservationsPourClient(int clientId, bool IsClotured =false)
        {
            var clientReservations = _context.Reservation
            .Include(r => r.PlaceParking)
                .ThenInclude(pp => pp.Parking)
                    .ThenInclude(p => p.Adresse)
            .Where(r => r.ClientId == clientId && r.RES_Cloture== IsClotured)
            .ToList();


            return clientReservations;
        }
        public async Task<Reservation> GetReservationsUpdateClient( Reservation reservation )
        {
            var clientReservation = await _context.Reservation
            .Include(r => r.PlaceParking)
                .ThenInclude(pp => pp.Parking)
                    .ThenInclude(p => p.Adresse) // Charger les informations sur l'adresse du parking
            .FirstOrDefaultAsync(r => r.ClientId == reservation.ClientId && r.RES_Id == reservation.RES_Id);

            return clientReservation;
        }
        //public async Task<bool> ReservationsDunePlace(int placeId, DateTime dateDebut, DateTime dateFin)
        //{
        //    var reservationsForPlace = _context.Reservation
        //        .AsNoTracking()
        //        .Where(r => r.PlaceId == placeId)
        //        .ToList();

        //    if (reservationsForPlace.Any())
        //    {
        //        foreach (var reservation in reservationsForPlace)
        //        {
        //            if (dateDebut <= reservation.RES_DateFin && dateFin >= reservation.RES_DateDebut)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}
        public async Task<bool> ReservationsDunePlace(int placeId, DateTime dateDebut, DateTime dateFin)
        {
            //bool ok = false;

            //ok = await _context.Reservation
            //.AnyAsync(r => r.PlaceId == placeId &&
            //          ((r.RES_DateDebut <= dateDebut && r.RES_DateFin >= dateDebut)||
            //          (r.RES_DateDebut <= dateFin && r.RES_DateFin >= dateFin)));

            //return !ok;
            var isReserved = await _context.Reservation
                .AnyAsync(r => r.PlaceId == placeId && !r.RES_Cloture && (
                    (r.RES_DateDebut <= dateDebut && r.RES_DateFin >= dateDebut) ||
                    (r.RES_DateDebut <= dateFin && r.RES_DateFin >= dateFin) ||
                    (r.RES_DateDebut >= dateDebut && r.RES_DateFin <= dateFin)
                ));

            return isReserved;

        }
        //public async Task UpdateResCloture(int clientId, int resId)
        //{
        //    var reservation = await _context.Reservation
        //    .Where(r => r.ClientId == clientId && r.RES_Id == resId)
        //    .FirstOrDefaultAsync();

        //    reservation.RES_Cloture = false;
        //    _context.Reservation.Update(reservation);
        //    //await _context.Reservation.ExecuteUpdateAsync(r => r.clientId && r.RES_Id == resId)
        //}

        public async Task<Reservation> UpdateResCloture(Reservation reservation)
        {
            var res = await _context.Reservation
            //.Where(r => r.ClientId == reservation.ClientId && r.RES_Id == reservation.RES_Id)
            .Where(r => r.RES_Id == reservation.RES_Id)
            .FirstOrDefaultAsync();

            if(res == null)
            {
                return reservation;
            }
            res.RES_Cloture = reservation.RES_Cloture;
            res.FactureId = reservation.FactureId;
            //_context.Reservation.Update(res);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task UpdatereservationDelete(int resId,int factureId)
        {
            var reservation = await _context.Reservation
            .Where(r =>  r.RES_Id == resId)
            .FirstOrDefaultAsync();
           
            reservation.RES_Cloture  = true;
            reservation.FactureId = factureId;
            _context.Reservation.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation> UpdateReservationClient(Reservation reservation)
        {
            
            if(reservation == null) { throw new ArgumentException("La reservation est vide !!!"); }
            else
            {
                _context.Reservation.Update(reservation);
                await _context.SaveChangesAsync();
            }          
            return reservation;
        }

        public Task<StatReservationDebFinBO> GetReservationsDebutEtFin(int parkingId, DateTime date)
        {
            DateTime dateDebut = date.Date;  // Début du jour J (00:00:00)
            DateTime dateFin = date.Date.AddDays(1).AddTicks(-1); // Fin du jour J (23:59:59.9999999)

            // Récupérer le nombre de réservations qui commencent le jour J dans le parking donné
            int reservationsCommencent = _context.Reservation
                .Count(r => r.PlaceParking.ParkingId == parkingId &&
                            r.RES_DateDebut >= dateDebut && r.RES_DateDebut <= dateFin && r.RES_Cloture == false);

            // Récupérer le nombre de réservations qui se terminent le jour J dans le parking donné
            int reservationsTerminent = _context.Reservation
                .Count(r => r.PlaceParking.ParkingId == parkingId &&
                            r.RES_DateFin >= dateDebut && r.RES_DateFin <= dateFin && r.RES_Cloture == false);

            var result = new StatReservationDebFinBO
            {
                ReservationsCommencent = reservationsCommencent,
                ReservationsTerminent = reservationsTerminent
            };

            return Task.FromResult(result);
        }

        public async Task<int> GetReservationNombre(int parkingId, DateTime date)
        {
            int nombreDeReservations = _context.Reservation
                      .Where(r => r.PlaceParking.ParkingId == parkingId &&
                         r.RES_DateDebut.Date <= date.Date &&
                         (r.RES_DateFin == null || r.RES_DateFin.Value.Date >= date.Date))
                      .Count();
            return nombreDeReservations;
        }

        public async Task<int> GetReservationNombreDuMois(int parkingId, DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

           
            var nombreDeReservations = _context.Reservation
                .Where(r => r.PlaceParking.ParkingId == parkingId &&
                            r.RES_DateReservation >= firstDayOfMonth &&
                            r.RES_DateReservation <= lastDayOfMonth)
                .Count();
            return nombreDeReservations;
        }

        public async Task<List<Reservation>> GetReservationsByParking(int parkingId)
        {
            var nonCloturedReservations = await _context.Reservation
                .AsNoTracking()
                .Where(r => r.RES_Cloture == false && r.PlaceParking.ParkingId == parkingId)
                .Include(r => r.Client)
                    .ThenInclude(c => c.Cli) // Inclure la personne liée au client
                .Include(r => r.PlaceParking)
                .ToListAsync();

            return nonCloturedReservations;
        }

        public async Task<List<StatReservationParMois>> GetStatReservation()
        {
            var reservationsPerMonth = await _context.Reservation
            .Where(r => r.RES_DateReservation.Year == DateTime.Now.Year)
            .GroupBy(r => new { r.RES_DateReservation.Year, r.RES_DateReservation.Month })
            .Select(g => new StatReservationParMois
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                ReservationNombre = g.Count(),
                TotalDuree = g.Sum(r => r.RES_DureeTotal_Initiale)
            })
            .OrderBy(g => g.Year).ThenBy(g => g.Month)
            .ToListAsync();

            return reservationsPerMonth;
        }
    }
}
