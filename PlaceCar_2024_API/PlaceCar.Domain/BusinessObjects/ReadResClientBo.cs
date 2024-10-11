using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadResClientBo
    {
        public int RES_Id { get; set; }
        public DateTime RES_DateReservation { get; set; }
        public DateTime RES_DateDebut { get; set; }
        public DateTime? RES_DateFin { get; set; }
        public int PLA_Etage { get; set; }
        public int PLA_NumeroPlace { get; set; } 
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; } 
        public int ADRS_Numero { get; set; }
        public string ADRS_NomRue { get; set; }
        public string ADRS_Ville { get; set; } 
    }
}
