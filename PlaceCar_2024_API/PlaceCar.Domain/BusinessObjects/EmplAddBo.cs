using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class EmplAddBo
    {
        public string UTIL_Nom { get; set; }
        public string UTIL_Prenom { get; set; }
        public DateTime UTIL_DateNaissance { get; set; }
        public string UTIL_Email { get; set; }
        public string UTIL_Password { get; set; }
        
        public ParkingEntity Parking { get; set; }
    }
}
