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
    public class EmployeeRepository : IEmployeRepository
    {
        private readonly PlaceCarDbContext _context;

        public EmployeeRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee empl,ParkingEntity park)
        {
           
            try
            {
                if (empl != null)
                {
                    _context.Employee.Add(empl); 
                    _context.EmployeWorkOn.Add(new EmployeWorkOn() { Employee = empl, Parking = park });
                    _context.Entry<ParkingEntity>(park).State = EntityState.Unchanged; 
                }
                else { throw new ArgumentNullException(nameof(empl)); }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteEmployeInfo(int empId)
        {
            var employee = await _context.Employee.FindAsync(empId);

            if (employee != null) { _context.Employee.Remove(employee); }
            else { throw new InvalidOperationException("L'employé n'a pas été trouvé."); }
        }

        public async Task UpdateEmployeInfo(UpdateClientBO updateClientBO)
        {
            var employee = await _context.Employee
                .Include(c => c.Emp_Pers_Id)
                .SingleOrDefaultAsync(c => c.Emp_Pers_Id == updateClientBO.Client_Id);

            if (employee != null)
            {
                employee.EmpPers.PERS_Email = updateClientBO.UTIL_Email;
                employee.EmpPers.PERS_Password = updateClientBO.UTIL_Password;

                _context.Employee.Update(employee);
            }
            else { throw new InvalidOperationException("Le client n'a pas été trouvé."); }
        }

        public async Task Update_Emp_WorkIn(int empId, int parkId)
        {
            var employee = await _context.Employee
                                 .AsNoTracking()
                                 .Include(e => e.EmployeWorkOns)
                                 .SingleOrDefaultAsync(e => e.Emp_Pers_Id == empId);

            if(employee != null) 
            {
                var parking = await _context.Parking.FindAsync(parkId);
                if(parking != null) 
                { 
                    //employee.Parkings.Clear();
                    //employee.Parkings.Add(parking);

                    _context.Employee.Add(employee);
                }
            }
            else { throw new InvalidOperationException("L'employé n'a pas été trouvé."); }
        }

        public async Task<List<Employee>> GetAllEmpInParking(int parkingId)
        {
            try
            {
                if (parkingId == null) { throw new ArgumentException("Id du Parking est vide !!!"); }
                else
                {
                    var employesDansParking = _context.Employee
                      .Where(e => e.EmployeWorkOns.Any(ewo => ewo.ParkingId == parkingId))
                      .ToList();
                    return employesDansParking;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var emp = await _context.Employee
                           .Include(e => e.EmpPers)
                           .FirstOrDefaultAsync(e => e.Emp_Pers_Id == employeeId);

            return emp;
        }

        public async Task DeleteEmployee(int personId)
        {
            try
            {
                var employee = await _context.Employee
                  .FirstOrDefaultAsync(e => e.Emp_Pers_Id == personId);

                if (employee == null) { throw new InvalidOperationException("L'employé n'a pas été trouvé."); }
                else
                {
                    _context.Employee.Remove(employee);

                    var person = await _context.Personne.FirstOrDefaultAsync(p => p.PERS_Id == personId);
                    person.Employee = null;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> GetEmployeeNombreTotal()
        {
            return await _context.Employee.CountAsync();
        }

        //public async Task<List<Employee>> GetAllEmplyees()
        //{
        //    try
        //    {
        //        var employees = await _context.Employee
        //            .AsNoTracking()
        //            .Include(e => e.EmpPers)
        //            .Include(e => e.EmployeWorkOns)
        //                .ThenInclude(ewo => ewo.Parking)
        //            .ToListAsync();

        //        return employees;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
