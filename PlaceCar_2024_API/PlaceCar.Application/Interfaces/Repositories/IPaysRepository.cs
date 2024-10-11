using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IPaysRepository
    {
        Task<List<PaysEntity>> GetAllPays();
        Task AddPays(PaysEntity pays);
        Task<bool> PaysExiste(string paysNom);
        Task<PaysEntity> GetPaysById(int id);
        
       
    }
}
