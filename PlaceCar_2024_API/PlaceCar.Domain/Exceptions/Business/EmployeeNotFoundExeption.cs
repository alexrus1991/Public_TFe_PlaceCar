using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class EmployeeNotFoundExeption : Exception
    {
        public EmployeeNotFoundExeption(int employeeId) : base($"Employee avec identifient {employeeId} ne travaille pas dans le Group PlaceCar")
        {
        }
    }
}
