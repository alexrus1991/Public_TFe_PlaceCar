using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class PlacesRepository : IPlacesRepository
    {
        private readonly PlaceCarDbContext _context;

        public PlacesRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddPlaceParking(PlaceParking placeParking)
        {
            try
            {
                if(placeParking != null) { _context.PlaceParking.Add(placeParking); }
                else { throw new ArgumentNullException(nameof(placeParking)); }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PlaceParking> GetPlaceById(int placeId)
        {
            var place = await _context.PlaceParking
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.PLA_Id == placeId);
            return place;
        }

        public async Task<List<PlaceParking>> GetPlacesLibresInParking(int parkingId, DateTime date)
        {

            var placesLibres = await _context.PlaceParking
        .Include(p => p.Parking)
        .Include(p => p.Reservation)
        .Where(p => p.ParkingId == parkingId)
        .Where(p => !p.Reservation
            .Any(r=>r.RES_Cloture != true && r.PlaceId == p.PLA_Id))
        .ToListAsync();

            return placesLibres;           
        }

        //public async Task<List<PlaceStatusBO>> GetOccupationPlacesForFloor(int parkingId, int etageNum, DateTime date)
        //{
            
        //    // Récupère les réservations pour la date donnée
        //    var reservations = await _context.Reservation
        //        .Where(r => r.PlaceParking.ParkingId == parkingId &&
        //                    r.PlaceParking.PLA_Etage == etageNum &&
        //                    (r.RES_DateDebut.Date <= date.Date && r.RES_DateFin >= date) && r.RES_Cloture == false)//||(r.RES_DateDebut <= date && r.RES_DateFin >= r.RES_DateDebut)  && r.RES_Cloture == false

        //        .ToListAsync();

        //    // Récupère toutes les places de parking sur l'étage spécifié
        //    var parkingPlaces = await _context.PlaceParking
        //        .Where(p => p.ParkingId == parkingId && p.PLA_Etage == etageNum)
        //        .ToListAsync();

        //    // Crée la liste des statuts des places
        //    var placeStatuses = parkingPlaces.Select(place =>
        //    {
        //        // Cherche une réservation pour cette place
               
        //        var reservation = reservations.FirstOrDefault(r => r.PlaceId == place.PLA_Id);

        //        int status;


        //        if (reservation != null)
        //        {
        //            //Orange = status 2
        //            if (reservation.RES_DateFin.HasValue && reservation.RES_DateFin.Value.Date == date.Date)
        //            {
        //                status = 2;
        //            }
                   
        //            else
        //            {
        //                status = 0;
        //            }
        //        }
        //        else
        //        {
        //            status = 1; // La place est libre
        //        }


        //        return new PlaceStatusBO
        //        {
        //            PlaceId = place.PLA_Id,
        //            NumeroPlace = place.PLA_NumeroPlace,
        //            Status = status
        //        };
        //    }).ToList();

        //    return placeStatuses;
        //}

        public async Task<List<PlaceStatusBO>> GetOccupationPlacesForFloor(int parkingId, int etageNum, DateTime date, DateTime? dateDebut, DateTime? dateFin)
        {
            List<Reservation> reservations;

            // Cas 1 : si une plage de dates est fournie (dateDebut et dateFin), vérifiez le chevauchement, le comportement pour la partie client
            if (dateDebut.HasValue && dateFin.HasValue)
            {
                reservations = await _context.Reservation
                    .Where(r => r.PlaceParking.ParkingId == parkingId &&
                               r.PlaceParking.PLA_Etage == etageNum &&
                                r.RES_Cloture == false) //&&
                                //(
                                //    // Conditions pour vérifier les chevauchements de réservations
                                //    (r.RES_DateDebut <= dateFin.Value && r.RES_DateFin >= dateDebut.Value) ||
                                //    (r.RES_DateDebut <= dateDebut.Value && r.RES_DateFin >= dateDebut.Value) ||
                                //    (r.RES_DateDebut <= dateFin.Value && r.RES_DateFin >= dateFin.Value)
                                //))
                    .ToListAsync();
                //reservations = await _context.Reservation
                //    .Where(r => r.PlaceParking.ParkingId == parkingId &&
                //                r.PlaceParking.PLA_Etage == etageNum &&
                //                r.RES_Cloture == false && // Place considérée comme occupée si non clôturée
                //                (
                //                    // Conditions pour vérifier les chevauchements OU une réservation non clôturée
                //                    (r.RES_DateDebut <= dateFin.Value && r.RES_DateFin >= dateDebut.Value) //||
                //                    //r.RES_Cloture == false
                //                ))
                //    .ToListAsync();
            }
            // Cas 2 : si une seule date est fournie le comportement pour la partie employé
           else
            {
                reservations = await _context.Reservation
                    .Where(r => r.PlaceParking.ParkingId == parkingId &&
                                r.PlaceParking.PLA_Etage == etageNum &&
                                (r.RES_DateDebut.Date <= date.Date && r.RES_DateFin >= date) &&
                                r.RES_Cloture == false)
                    .ToListAsync();
            }

            // Récupère toutes les places de parking sur l'étage spécifié
            var parkingPlaces = await _context.PlaceParking
                .Where(p => p.ParkingId == parkingId && p.PLA_Etage == etageNum)
                .ToListAsync();

            // Crée la liste des statuts des places
            var placeStatuses = parkingPlaces.Select(place =>
            {
                // Cherche une réservation pour cette place
               var reservation = reservations.FirstOrDefault(r => r.PlaceId == place.PLA_Id);

                int status;

                if (reservation != null)
                {                   
                   // Orange = status 2, indique la date de fin de la réservation
                    if (reservation.RES_DateFin.HasValue && reservation.RES_DateFin.Value.Date == date.Date)
                    {
                        status = 2;
                    }
                    //else if(reservation.RES_Cloture == false && reservation.RES_DateFin.Value.Date <= date.Date) { status = 2; }
                        
                    else
                    {
                        status = 0; // Place occupée
                    }
                }
                else
                {
                    status = 1; // Place libre
                }

                return new PlaceStatusBO
                {
                    PlaceId = place.PLA_Id,
                    NumeroPlace = place.PLA_NumeroPlace,
                    Status = status
                };
            }).ToList();

            return placeStatuses;
        }
        public Task<StatPlacesBo> GetOccupationPlacesForDay(int parkingId, DateTime date)
        {
            var reservations = _context.Reservation
        .Where(r => r.PlaceParking.ParkingId == parkingId &&
                    (r.RES_DateDebut <= date.Date && r.RES_DateFin >= date.Date))
        .ToList();

            // Récupère les informations du parking, y compris les places de parking
            var parking = _context.Parking
                .Include(p => p.Places)
                .Single(p => p.PARK_Id == parkingId);

            int totalPlaces = parking.Places.Count;

            int placesOccupees = reservations.Count;

            int placesLibres = totalPlaces - placesOccupees;

            var result = new StatPlacesBo
            {
                Occupees = placesOccupees,
                Libres = placesLibres
            };

            return Task.FromResult(result);
           
        }

        public Task<StatTauxReservationParEtageBo> GetReservationStatParEtage(int parkingId, int etagenumero, DateTime date)
        {
            var totalPlacesEtage = _context.PlaceParking
           .Count(pp => pp.ParkingId == parkingId && pp.PLA_Etage == etagenumero);

            // Récupérer le nombre de places réservées sur cet étage pour la date donnée
            var placesReservees = _context.Reservation
                .Count(r => r.PlaceParking.ParkingId == parkingId &&
                                 r.PlaceParking.PLA_Etage == etagenumero &&
                                 r.RES_DateDebut.Date <= date.Date &&
                                 r.RES_DateFin.Value.Date >= date.Date);

            
            // Calculer le nombre de places non réservées sur cet étage pour la date donnée
            var placesNonReservees = totalPlacesEtage - placesReservees;

            // Calculer le pourcentage de places réservées et non réservées sur cet étage
            double pourcentageReservees = totalPlacesEtage > 0 ? (double)placesReservees / totalPlacesEtage * 100 : 0;
            double pourcentageNonReservees = totalPlacesEtage > 0 ? (double)placesNonReservees / totalPlacesEtage * 100 : 0;

            var result = new StatTauxReservationParEtageBo
            {
                PlacesReservees = placesReservees,
                PlacesNonReservees = placesNonReservees,
                PourcentagePlacesReservees = pourcentageReservees,
                PourcentagePlacesNonReservees = pourcentageNonReservees
            };

            return Task.FromResult(result);
        }

        public async Task UpdatePlaceParking(PlaceParking place, Reservation reservation)
        {
            try
            {
                var pl = await _context.PlaceParking
                .Where(p => p.PLA_Id == place.PLA_Id)
                .FirstOrDefaultAsync();

                pl.Reservation.Add(reservation);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePlaceParkinglibre(PlaceParking place, Reservation reservation)
        {
            try
            {
                var pl = await _context.PlaceParking
                .Where(p => p.PLA_Id == place.PLA_Id)
                .FirstOrDefaultAsync();

                pl.Reservation.Remove(reservation);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        //// Fetch reservations for the given date
        //var reservations = await _context.Reservation
        //    .Where(r => r.PlaceParking.ParkingId == parkingId &&
        //                r.PlaceParking.PLA_Etage == etageNum &&
        //                (r.RES_DateDebut <= date && r.RES_DateFin >= date))
        //    .ToListAsync();

        //// Fetch all parking places on the specified floor
        //var parkingPlaces = await _context.PlaceParking
        //    .Where(p => p.ParkingId == parkingId && p.PLA_Etage == etageNum)
        //    .ToListAsync();

        //var placeStatuses = parkingPlaces.Select(place =>
        //{
        //    // Check if the place is reserved or occupied
        //    var reservation = reservations.FirstOrDefault(r => r.PlaceId == place.PLA_Id);

        //    int status;
        //    if (reservation != null)
        //    {
        //        // Check if the reservation ends on the specified date
        //        status = reservation.RES_DateFin == date.Date ? 2 : 0;
        //    }
        //    else
        //    {
        //        status = 1; // Free
        //    }

        //    return new PlaceStatusBO
        //    {
        //        PlaceId = place.PLA_Id,
        //        NumeroPlace = place.PLA_NumeroPlace,
        //        Status = status
        //    };
        //}).ToList();

        //return placeStatuses;

        //---------------------------------------------------------------------------------------
        //var reservations = _context.Reservation
        //    .Where(r => r.PlaceParking.ParkingId == parkingId &&
        //                ((r.RES_DateDebut <= date && r.RES_DateFin >= date) ||
        //           (r.RES_DateDebut <= date && r.RES_DateFin >= date) ||
        //           (r.RES_DateDebut >= date && r.RES_DateFin <= date));

        //    .ToList();


        //var parking = _context.Parking
        //    .Include(p => p.Places)
        //    .Single(p => p.PARK_Id == parkingId);

        //int totalPlaces = parking.Places.Count;


        //int placesOccupees = reservations.Count;


        //int placesLibres = totalPlaces - placesOccupees;

        //var result = new  StatPlacesBo{
        //    Occupees = placesOccupees,
        //    Libres = placesLibres 
        //};
        //return Task.FromResult(result);

        //---------------------------------------
        //var reservation = reservations.FirstOrDefault(r => r.PlaceId == place.PLA_Id);

        //int status;
        //if (reservation != null)
        //{
        //    // Comparer uniquement les dates (ignorer l'heure)
        //    if (reservation.RES_DateFin.HasValue && reservation.RES_DateFin.Value.Date == date.Date)
        //    {
        //        status = 2; // La place est sur le point d'être libérée
        //    }
        //    else
        //    {
        //        status = 0; // La place est réservée (occupée)
        //    }
        //}
        //else
        //{
        //    status = 1; // La place est libre
        //}
    }


}
