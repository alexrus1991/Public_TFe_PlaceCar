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
    public interface IAdminService
    {
        Task AddNewPays(string nomPays);
        //Task AddEmplyee(AddEmpBO empBO, bool Admin = false);
        Task AddParking(ParkingBO parkingBO);
        Task<List<EmpWorkOnBO>> GetAllEmpWorkOnBOs(int parkingId);
        Task<List<EmpWorkOnBO>> GetAllEmplyeesWorkIn();
        //Task<List<EmpWorkOnBO>> GetAllEmplyees(int userId);
        Task<EmpWorkOnBO> UpdateEmployeeWorkOnParking(int employeeId, int parkingId);
        Task DeleteEmpWorkOn(int employeeId,int parkingId);
        Task DeleteEmployéé(int employeeId);
        Task<int> GetEmployeeNombre();
        Task<int> GetNombreTotalParkings();
        Task<int> GetEmployeeNombreByParking(int parkingId);
        Task<List<StatReservationParMois>> GetResStatParMois();
        Task<List<StatTransacParMois>> GetTransStatParMois();
    }
}
