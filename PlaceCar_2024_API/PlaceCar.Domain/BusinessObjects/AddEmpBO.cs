using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddEmpBO
    {
        public string PERS_Nom { get; set; }
        public string PERS_Prenom { get; set; }
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; }
        public string PERS_Password { get; set; }
        public int ParkingId { get; set; }
       // public int roleId { get; set; }
    }
}
