using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class CompteService : ICompteService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public CompteService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        public async Task<ReadCompteCliBO> GetCompteByClientId(int clientId)
        {
            try
            {
                ReadCompteCliBO cb = new ReadCompteCliBO();
                var client = await unitOfWork.Client.GetClientById(clientId);
                if(client == null) { throw new ArgumentException("Le client spécifié n'existe pas!!"); }
                else
                {
                    var compte = await unitOfWork.Compte.GetCompteClient(client.Cli_Id);
                    if(compte == null) { throw new ArgumentException("Le client ne possede pas de compte !!"); }
                    if(compte.ClientId != client.Cli_Id) { throw new ArgumentException("Le compte n'appartien pas au client !!"); }
                    else
                    {
                        cb = mapper2.Map<ReadCompteCliBO>(compte);                      
                    }
                    await unitOfWork.SaveAsync();
                }
                return cb;
            }
            catch (Exception ex)
            { 

                throw;
            }
        }
    }
}
