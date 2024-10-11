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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PlaceCarDbContext _context;

        public TransactionRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task<Trensaction> AddTransaction(Trensaction trensaction)
        {
            try
            {
                if(trensaction == null) { throw new ArgumentException("La transaction est vide !!!"); }
                else
                {
                    trensaction.TRANS_Date = DateTime.Now;
                    
                    _context.Trensaction.Add(trensaction);
                    
                    await _context.SaveChangesAsync();                 
                }
                return trensaction;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Trensaction>> GetAllTransactionsClient(int clientId)
        {
            var trs = await _context.Trensaction
                .AsNoTracking()
                .Include(t => t.CompteUn)
                    .ThenInclude(c => c.Client)
                .Include(t => t.CompteEntrepriseNavigation) // Inclure la table InfoEntreprise
                .Where(t => t.CompteUn.ClientId == clientId)
                .ToListAsync();

            return trs;

            //MAIS plus difficile a recupérer correctement une liste de transactions
            //return await _context.Client
            //       .Include(c => c.Comptes)
            //       .ThenInclude(cc => cc.Transactions)
            //       .Select(t => t.Comptes.Transactions.ToList()).ToListAsync();
        }

        public async Task<List<Trensaction>> GetAllTransactionsPlacecar(DateTime date)
        {
            var transactionDetailsList = _context.Trensaction
            .Include(t => t.CompteUn)
                .ThenInclude(c => c.Client)
                    .ThenInclude(cli => cli.Cli)
                    //.ThenInclude(p => p.Client) // Assuming Personne is related to Client
            .Include(t => t.CompteEntrepriseNavigation)
            .Include(t => t.Facture)
                .ThenInclude(f => f.Reservation)
                    .ThenInclude(r => r.PlaceParking)
                        .ThenInclude(pp => pp.Parking)
                .Where(t => t.TRANS_Date.Date.Year == date.Date.Year && t.TRANS_Date.Date.Month == date.Date.Month && t.TRANS_Date.Date.Day == date.Date.Day)

        .ToList();
            return transactionDetailsList;


        }

        public async Task<Trensaction> GetTransactionById(int transacId)
        {
            var trs = await _context.Trensaction
                    .AsNoTracking()
                    .Where(r => r.TRANS_Id == transacId )
                    .FirstOrDefaultAsync();

            return trs;
        }

        public async Task<Trensaction> UpdateTransactionNoValide(Trensaction trensaction)
        {
            try
            {
                if (trensaction == null) { throw new ArgumentException("L'objet transaction est vide !!!"); }
                else
                {
                    //Pour stripe
                    if (_context.Database.GetDbConnection().State == System.Data.ConnectionState.Closed) _context.Database.GetDbConnection().Open();
                    _context.Trensaction.Update(trensaction);
                    await _context.SaveChangesAsync();
                    return trensaction;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Trensaction> DeleteTransaction(Trensaction trensaction)
        {
            try
            {
                if (trensaction == null) { throw new ArgumentException("L'Entité Transaction spécifié est vide !!!"); }
                else
                {
                    var trs = _context.Trensaction
                    .FirstOrDefault(p => p.TRANS_Id == trensaction.TRANS_Id );

                    if (trs != null)
                    {
                        _context.Trensaction.Remove(trs);
                    }
                    return trensaction;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<StatTransacParMois>> GetStatTransaction()
        {
            var transactionsPerMonth = await _context.Trensaction
           .Where(t => t.TRANS_Date.Year == DateTime.Now.Year)
           .GroupBy(t => new { t.TRANS_Date.Year, t.TRANS_Date.Month })
           .Select(g => new StatTransacParMois
           {
               Year = g.Key.Year,
               Month = g.Key.Month,
               TransactionNombre = g.Count(),
               TotalSomme = g.Sum(t => t.TRANS_Somme)
           })
           .OrderBy(g => g.Year).ThenBy(g => g.Month)
           .ToListAsync();

            return transactionsPerMonth;
        }
    }
}
