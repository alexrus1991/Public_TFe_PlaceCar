using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class FactureRepository : IFactureRepository
    {
        private readonly PlaceCarDbContext _context;

        public FactureRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<Facture> AddFacture(Facture facture)
        {
            try
            {
                if (facture == null) { throw new ArgumentNullException(nameof(facture)); }
                else
                {
                    facture.StripeConfirmStr = "";
                    facture.Status = false;
                    //facture.FACT_Date = DateTime.Now;
                    _context.Facture.Add(facture);
                    await _context.SaveChangesAsync();
                }
                return facture;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Facture>> GetAllFacturesClient(int clientId)
        {
            var rep = await _context.Facture
                .AsNoTracking()
                .Include(f => f.Reservation)
                    .ThenInclude(r => r.Client) 
                .Where(f => f.Reservation.ClientId == clientId && f.Status == false)
                .ToListAsync();
            return rep;
        }

        public async Task<List<Facture>> GetAllFacturesClientPaye(int clientId)
        {
            var rep = await _context.Facture
               .AsNoTracking()
               .Include(f => f.Reservation)
                   .ThenInclude(r => r.Client) 
               .Where(f => f.Reservation.ClientId == clientId && f.Status == true)
               .ToListAsync();
            return rep;
        }

        public async Task<Facture> GetFactureById(int factureId)
        {
            try
            {
                //Pour stripe
                //if (_context.Database.GetDbConnection().State == System.Data.ConnectionState.Closed) _context.Database.GetDbConnection().Open();
                    var f = await _context.Facture
                       //.AsNoTracking()
                       .Include(f => f.Reservation)
                       .ThenInclude(r => r.PlaceParking)
                       .FirstOrDefaultAsync(p => p.FACT_Id == factureId);
                return f;
            }
            catch (Exception ex)
            {

                return null;
            }

         
        }

        

        public async Task<Facture> UpdateFacture(Facture facture)
        {
            try
            {
                if (facture == null) { throw new ArgumentException("L'objet facture est vide !!!"); }
                else
                {
                    //Pour stripe
                    if (_context.Database.GetDbConnection().State == System.Data.ConnectionState.Closed) _context.Database.GetDbConnection().Open();
                    _context.Facture.Update(facture);
                    await _context.SaveChangesAsync();
                    return facture;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public async Task<Facture> UpdateFactureStripe(Facture facture)
        //{
        //    try
        //    {
        //        if (facture == null) { throw new ArgumentException("L'objet facture est vide !!!"); }
        //        else
        //        {
        //            _context.Facture.Update(facture);
        //            await _context.SaveChangesAsync();
        //            return facture;
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task<StatFacturesBO> GetStatFactures(int parkingId, DateTime dateDonee)
        {
            var facturesQuery = _context.Facture
        .Include(f => f.Reservation)
            .ThenInclude(r => r.PlaceParking)
                .ThenInclude(pp => pp.Parking) 
        .Where(f => f.Reservation.PlaceParking.Parking.PARK_Id == parkingId);
        //&&
        //            dateDonee >= f.Reservation.RES_DateDebut &&
        //            dateDonee <= f.Reservation.RES_DateFin);

            // Compter les factures payées
            var factPayee = await facturesQuery
                .CountAsync(f => f.Status.HasValue && f.Status.Value);

            // Compter les factures non payées
            var factNonPaye = await facturesQuery
                .CountAsync(f => f.Status.HasValue && !f.Status.Value);

            // Retourner le résultat
            var result = new StatFacturesBO
            {
                factPayee = factPayee,
                factNonPaye = factNonPaye
            };

            return result;
            //var factPayee = _context.Facture
            //.Include(f => f.Reservation)
            //.ThenInclude(r => r.PlaceParking)
            //.ThenInclude(pp => pp.Parking)
            //.Include(f => f.Trensaction)
            //.Count(f => f.Reservation.PlaceParking.Parking.PARK_Id == parkingId &&
            //            (f.Status.HasValue && f.Status.Value) &&
            //            dateDonee >= f.Reservation.RES_DateDebut &&
            //            dateDonee <= f.Reservation.RES_DateFin);

            //var factNonPaye = _context.Facture
            // .Include(f => f.Reservation)
            // .ThenInclude(r => r.PlaceParking)
            // .ThenInclude(pp => pp.Parking)
            // .Include(f => f.Trensaction)
            // .Count(f => f.Reservation.PlaceParking.Parking.PARK_Id == parkingId &&
            //            (f.Status.HasValue && !f.Status.Value) &&
            //            dateDonee >= f.Reservation.RES_DateDebut &&
            //            dateDonee <= f.Reservation.RES_DateFin);

            //var result = new StatFacturesBO
            //{
            //    factPayee = factPayee,
            //    factNonPaye = factNonPaye
            //};
            //return Task.FromResult(result);
        }

       
    }
}
