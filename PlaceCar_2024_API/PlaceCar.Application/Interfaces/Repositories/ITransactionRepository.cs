using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Trensaction> AddTransaction(Trensaction trensaction);
        Task<Trensaction> GetTransactionById(int transacId);
        Task<List<Trensaction>> GetAllTransactionsClient(int clientId);
        Task<List<Trensaction>> GetAllTransactionsPlacecar(DateTime date);
        Task<List<StatTransacParMois>> GetStatTransaction();
        Task<Trensaction> UpdateTransactionNoValide(Trensaction trensaction);
        Task<Trensaction> DeleteTransaction(Trensaction trensaction);
    }
}
