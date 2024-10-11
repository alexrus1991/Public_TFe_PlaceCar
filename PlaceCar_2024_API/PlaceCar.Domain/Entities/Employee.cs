using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Employee
    {
        public int Emp_Pers_Id { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Personne EmpPers { get; set; } = null!;

        public List<EmployeWorkOn> EmployeWorkOns { get; set; } = [];
    }
}
