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
    public class ClientRepository : IClientRepository
    {
        private readonly PlaceCarDbContext _context;

        public ClientRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddClient(Client client)
        {
            try
            {
                if (client != null)
                {
                    _context.Client.Add(client);
                }
                else { throw new ArgumentNullException(nameof(client)); }
            }
            catch(Exception ex)
            {
                throw;
            }
            
          
        }
        public async Task<Client> GetClientById(int clientId)
        {
            var client = await _context.Client
                .Include(c => c.Cli).SingleAsync(c => c.Cli_Id == clientId);

            return client;


        }
        public async Task<Client> GetClientPersonneById(int personneId)
        {
 
            var client = await _context.Client
                .Include(c => c.Cli) // Inclut la table Personne
                .SingleOrDefaultAsync(c => c.Cli_Id == personneId);


            return client;
            
            
        }

        public async Task<int> GetClientNombreTotal()
        {
            return await _context.Client.CountAsync();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateClientBO"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Se déclenche lorsque le client n'est pas trouvé</exception>

        //public async Task<bool> UpdateClientInfo(int id,  string pwd)
        //{

        //    try
        //    {
        //        int reponse = _context.Client
        //                .Where(c => c.Cli_Id == id)
        //                .ExecuteUpdate(c => c
        //                        //.SetProperty(c => c.Cli.PERS_Email, mail)
        //                        .SetProperty(c => c.Cli.PERS_Password, pwd));
        //        var client = await _context.Client
        //            .Include(c => c.Cli_Id)
        //            .SingleOrDefaultAsync(c => c.Cli_Id == id);

        //        if (client != null) { return true; }
        //        else { throw new InvalidOperationException("Client does not exist");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
