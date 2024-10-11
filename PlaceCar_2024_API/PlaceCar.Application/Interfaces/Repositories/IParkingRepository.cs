using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IParkingRepository
    {
        Task AddParking(ParkingEntity parking);
        Task<List<Employee>> GetEmployeesByParkingId(int parkingId);

        Task<List<ParkingEntity>> GetParkingsByPaysEtVille(int paysId, string nomVille);
        Task<List<ParkingEntity>> GetParkingsParPays(int paysId);
        Task<List<string>> GetVillesParPays(int paysId);
        Task<ParkingEntity> GetParkingById(int parkingId);

        Task<List<PlaceParking>> GetPlacesLibresPourParking(int parkingId, DateTime date);
        Task<ParkingEntity> UpdateParking(ParkingEntity parking);
        Task<List<ParkingEntity>> GetParkings();
        Task<int> GetNombreParkingsPlaceCar();


    }
}
