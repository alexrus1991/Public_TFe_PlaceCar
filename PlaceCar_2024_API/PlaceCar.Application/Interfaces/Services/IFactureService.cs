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
    public interface IFactureService
    {
        //Task<Facture> AddFacture(Reservation reservation);
        // Task<Facture> RemoveFacture(int factureId);
        //Task<Facture> GetFactureById(int factureId);
        Task<Facture> AddFacture(Facture facture);
        Task UpdateFacture(Facture facture,decimal prix);
        Task<List<RaadFactureBO>> GetFacturesNonPaye(int clientId);
        Task<ReadFactureStripeBO> GetFactureStripe(int id);
        Task<List<RaadFactureBO>> GetFacturesPaye(int clientId);
        Task<StatFacturesBO> GetStatFacturesParJour(int parkingId, DateTime dateDonee);
        Task UpdateFactureStripe(int factureId, string stripeString, bool status);
    }
}
