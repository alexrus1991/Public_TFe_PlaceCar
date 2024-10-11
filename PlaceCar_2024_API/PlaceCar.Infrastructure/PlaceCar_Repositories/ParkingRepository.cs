using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly PlaceCarDbContext _context;

        public ParkingRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> InsertParking(ParkinInfo toInsert)
        //{
        //    _context.Adresse.Add(toInsert.Adresse);
        //    _context.Parking.Add(toInsert.Parking);
        //}
        public async Task AddParking(ParkingEntity pe)
        {
            try
            {
                if(pe == null) { throw new ArgumentException("Le parking spécifié est vide !!!"); }
                else
                {
                    _context.Parking.Add(pe);
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }    
            
        }

        public async Task<List<Employee>> GetEmployeesByParkingId(int parkingId)
        {
            var parking = await _context.Parking
                                    .AsNoTracking()
                                    //.Include("EmployeWorkOn")
                                    .Include(nameof(EmployeWorkOn))
                                    .FirstOrDefaultAsync(p => p.PARK_Id == parkingId);
            //.ToListAsync();
            return parking
                   .EmployeWorkOn.Select(e=>e.Employee)
                   .ToList();
        }

        public async Task<int> GetNombreParkingsPlaceCar()
        {
            return await _context.Parking.CountAsync();
        }

        public async Task<ParkingEntity> GetParkingById(int parkingId)
        {
            var parking = await _context.Parking
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.PARK_Id == parkingId);
            return parking;
        }

        public async Task<List<ParkingEntity>> GetParkings()
        {
            var parkings = await _context.Parking
            .AsNoTracking()
            .Include(p => p.Adresse)
            .ThenInclude(a => a.Pays)
            .ToListAsync();

            return parkings;
        }

        public async Task<List<ParkingEntity>> GetParkingsByPaysEtVille(int paysId, string nomVille)
        {
            var parkings = await _context.Parking
            .AsNoTracking()
            .Include(p => p.Adresse)
            .ThenInclude(a => a.Pays)
            .Where(p => p.Adresse != null && p.Adresse.Pays != null &&
                   p.Adresse.Pays.PAYS_Id == paysId &&
                   p.Adresse.ADRS_Ville == nomVille)
            .ToListAsync();

            return parkings;
        }

        public async Task<List<ParkingEntity>> GetParkingsParPays(int paysId)
        {
            var parkings = await _context.Parking
            .AsNoTracking()
            .Include(p => p.Adresse)
            .ThenInclude(a => a.Pays)
            .Where(p => p.Adresse != null && p.Adresse.Pays != null &&
                   p.Adresse.Pays.PAYS_Id == paysId)
            .OrderBy(p => p.Adresse.ADRS_Ville)
            .ToListAsync();

            return parkings;
        }
        public async Task<List<PlaceParking>> GetPlacesLibresPourParking(int parkingId, DateTime date)
        {
           

            return await _context.Parking
                 .AsNoTracking()
                 .Include(pl => pl.Places)
                     .ThenInclude(place => place.Reservation)
                 .Where(parking => parking.PARK_Id == parkingId)
                 .SelectMany(parking => parking.Places.Where(place =>
                     !place.Reservation.Any(reservation =>
                         (reservation.RES_DateDebut <= date && (reservation.RES_DateFin >= date || reservation.RES_DateFin == null)))                       
                     ))
                     .ToListAsync();
        }

        public async Task<List<string>> GetVillesParPays(int paysId)
        {
            var villes = await _context.Parking
            .AsNoTracking()
            .Include(p => p.Adresse)
                .ThenInclude(a => a.Pays)
            .Where(p => p.Adresse != null && p.Adresse.Pays != null && p.Adresse.Pays.PAYS_Id == paysId)
            .Select(p => p.Adresse.ADRS_Ville)
            .Distinct()
            .OrderBy(ville => ville)
            .ToListAsync();

            return villes;
        }

        public async Task<ParkingEntity> UpdateParking(ParkingEntity parking)
        {
            
            try
            {
                var pr = await _context.Parking
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.PARK_Id == parking.PARK_Id);
                    
                if (parking == null) { throw new ArgumentException("L'objet parking est vide !!!"); }
                else
                {
                    pr = parking;
                    //_context.Parking.Update(pr);
                    //await _context.SaveChangesAsync();
                    _context.Entry(pr).CurrentValues.SetValues(parking);
                    await _context.SaveChangesAsync();
                }
                return pr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    
}
