using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class PersonneRole
    {
        public int PersonneId { get; set; }
        public int RoleId { get; set; }

        public Personne Personne { get; set; }
        public Role Role { get; set; }
    }
}
