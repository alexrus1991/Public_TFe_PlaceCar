using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IEmpWorkOnRepository
    {
        Task AddEmpInParking(EmployeWorkOn employeWorkOn);
        Task<List<EmployeWorkOn>> GetAllEmplyeesInGroup();
        Task<List<EmployeWorkOn>> GetAllEmpInParking(int parkingId);
        Task<EmployeWorkOn> GetEmployeWorkOn(int employeeId);
        Task<EmployeWorkOn> GetEmpWorkOnByEmployeeId(int employeeId);
        Task<EmployeWorkOn> UpdateParkingEmpWork(EmployeWorkOn employeWork);
        Task DeleteEmployeeWorkOn(int employeeId, int parkingId);
        Task<int> GetEmployeeNombreInParking(int parkingId);
    }
}
