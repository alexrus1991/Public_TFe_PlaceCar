using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class EmployeWorkOn
    {
        public int Emp_Pers_Id { get; set; }

        public int ParkingId{get;set; }

        public Employee Employee { get; set; }
        public ParkingEntity Parking { get; set; }
    }
}
