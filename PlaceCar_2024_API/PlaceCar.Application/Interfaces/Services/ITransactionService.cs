using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task AddTrensaction(AddTransacBO transacBO);
        //Task<ReadTransacBO> AddTrensaction(AddTransacBO transacBO);
        Task<List<ReadTransacBO>> GetTrensactionsClient(int clientId);
        Task<List<ReadDeataiTransacBo>> GetAllTrensactions(DateTime date);
        Task UpdateTrensaction(int factureId, string description);

        
    }
}
