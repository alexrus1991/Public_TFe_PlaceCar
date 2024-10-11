using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IFactureRepository
    {
        Task<Facture> AddFacture(Facture facture);
        Task<Facture> GetFactureById(int factureId);
        Task<List<Facture>> GetAllFacturesClient(int clientId);
        Task<List<Facture>> GetAllFacturesClientPaye(int clientId);
        Task<Facture> UpdateFacture(Facture facture);
        //Task UpdateFacture(int factureId, decimal somme);

        Task<StatFacturesBO> GetStatFactures(int parkingId, DateTime dateDonee);
    }
}
