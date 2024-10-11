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
    public class EmpWorkOnRepository : IEmpWorkOnRepository
    {
        private readonly PlaceCarDbContext _context;

        public EmpWorkOnRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddEmpInParking(EmployeWorkOn employeWorkOn)
        {
            try
            {
                if (employeWorkOn == null) { throw new ArgumentNullException(nameof(employeWorkOn)); }
                else { _context.EmployeWorkOn.Add(employeWorkOn); }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteEmployeeWorkOn(int employeeId, int parkingId)
        {
            try
            {
                var employeWorkOn = await _context.EmployeWorkOn
                 .FirstOrDefaultAsync(ewo => ewo.Emp_Pers_Id == employeeId && ewo.ParkingId == parkingId);
                if (employeWorkOn != null)
                {
                    _context.EmployeWorkOn.Remove(employeWorkOn);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<EmployeWorkOn>> GetAllEmpInParking(int parkingId)
        {

            return await _context.EmployeWorkOn
                .AsNoTracking()
                .Include(pwo => pwo.Parking)
                .Include(pwo => pwo.Employee)
                .ThenInclude(e => e.EmpPers)
                .Where(pwo => pwo.ParkingId == parkingId)
                .ToListAsync();

        }

        public async Task<List<EmployeWorkOn>> GetAllEmplyeesInGroup()
        {
           var employeWorkOns = await _context.EmployeWorkOn
                .AsNoTracking()
                .Include(pwo => pwo.Parking)
                .Include(pwo => pwo.Employee)
                  .ThenInclude(e => e.EmpPers)
                  .ThenInclude(p => p.PersonneRoles)
                //.Where(pwo => pwo.Employee.EmpPers.PersonneRoles.Any(pr => pr.RoleId == 2))
                .ToListAsync();
            var filteredResult = employeWorkOns
                .Where(pwo => pwo.Employee.EmpPers.PersonneRoles.Any(pr => pr.RoleId == 2))
                .ToList();

            return filteredResult;
        }

        public async Task<int> GetEmployeeNombreInParking(int parkingId)
        {
            int nombreEmployes = _context.EmployeWorkOn
                .Where(ew => ew.ParkingId == parkingId)
                .Select(ew => ew.Employee)
                .Distinct()
                .Count();
            return nombreEmployes;
        }

        public async Task<EmployeWorkOn> GetEmployeWorkOn(int employeeId)
        {
            var employeeWorkOn = await _context.EmployeWorkOn
            .AsNoTracking()
            .FirstOrDefaultAsync(ewo => ewo.Emp_Pers_Id == employeeId);

            return employeeWorkOn;
        }

        public async Task<EmployeWorkOn> GetEmpWorkOnByEmployeeId(int employeeId)
        {
            var e = await _context.EmployeWorkOn
                .AsNoTracking()
                .Include(pwo => pwo.Parking)
                .Include(pwo => pwo.Employee)
                    .ThenInclude(e => e.EmpPers)
                .FirstOrDefaultAsync(pwo => pwo.Employee.Emp_Pers_Id == employeeId);

            return e;
        }

        public async Task<EmployeWorkOn> UpdateParkingEmpWork(EmployeWorkOn employeWork)
        {
            if (employeWork == null) { throw new ArgumentException("L'objet EmployeeWorkOn est vide !!!"); }
            else
            {
                //    var employeWorkOn = await _context.EmployeWorkOn
                //.FirstOrDefaultAsync(ewo => ewo.Employee.Emp_Pers_Id == employeWork.Emp_Pers_Id);

                //    employeWorkOn.ParkingId = employeWork.ParkingId;
                //    _context.EmployeWorkOn.Update(employeWorkOn);
                //    await _context.SaveChangesAsync();
                var employeWorkOn = await _context.EmployeWorkOn
              .FirstOrDefaultAsync(ewo => ewo.Employee.Emp_Pers_Id == employeWork.Emp_Pers_Id);

                if (employeWorkOn != null)
                {
                    _context.EmployeWorkOn.Remove(employeWorkOn);
                    await _context.SaveChangesAsync();

                    _context.EmployeWorkOn.Add(new EmployeWorkOn
                    {
                        Emp_Pers_Id = employeWork.Emp_Pers_Id,
                        ParkingId = employeWork.ParkingId
                    });


                    await _context.SaveChangesAsync();
                }
                return employeWork;

            }
        }
    }
}
