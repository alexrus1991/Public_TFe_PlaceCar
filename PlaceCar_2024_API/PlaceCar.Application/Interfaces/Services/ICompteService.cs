﻿using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface ICompteService
    {
        Task<ReadCompteCliBO> GetCompteByClientId(int clientId);
        //Task<List<CompteBank>> GetCompteBanksClients(int parkingId)
    }
}
