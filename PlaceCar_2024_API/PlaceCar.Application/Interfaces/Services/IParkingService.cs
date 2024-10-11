using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IParkingService
    {
        //Task<ParkingBO> AddParking(ParkingBO parkingBO);
        Task AddParking(ParkingBO parkingBO);
        Task AddFormPrix(AddFormuleBO formuleBO);
        Task<List<ReadPlacesLibreBO>> GetPlacesLibresInParking(int parkingId, DateTime date);
        Task<List<ReadParkVilleBO>> GetParkingsByPaysByVille(int paysId, string nomVille);
        Task<List<ReadParkVilleBO>> GetParkingsByPays(int paysId);
        Task<List<string>> GetVillesByPays(int paysId);
        Task<ParkingEmpWorkBo> GetParkingEmployee(int employeeId);
        Task<List<ReadAllParkingsBO>> GetParkingsAll();
        Task<ReadParkingBO> GetParkingById(int id);
    }
}
