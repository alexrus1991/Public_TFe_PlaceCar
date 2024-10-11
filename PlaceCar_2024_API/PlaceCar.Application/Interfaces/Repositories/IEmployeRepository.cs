using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IEmployeRepository
    {
        Task AddEmployee(Employee empl, ParkingEntity park);
        Task UpdateEmployeInfo(UpdateClientBO updateClientBO);
        Task Update_Emp_WorkIn(int empId,int parkId);
        Task DeleteEmployeInfo(int empId);
        Task<Employee> GetEmployeeById(int employeeId);
        //Task<List<Employee>> GetAllEmplyees();
        Task DeleteEmployee(int personId);
        Task<int> GetEmployeeNombreTotal();
        //Task<List<Employee>> GetAllEmpInParking(int parkingId);
    }
}
