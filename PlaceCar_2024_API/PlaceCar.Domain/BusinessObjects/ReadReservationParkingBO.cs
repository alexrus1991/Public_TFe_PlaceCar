using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadReservationParkingBO
    {
        public int RES_Id { get; set; }
        public DateTime RES_DateDebut { get; set; }
        public DateTime RES_DateFin { get; set; }
        public int PLA_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public int PERS_Id { get; set; }
    }
}
