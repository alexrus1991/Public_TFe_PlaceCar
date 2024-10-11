using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadPrefBO
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; }
        public int PLA_Etage { get; set; }
        public int PLA_NumeroPlace { get; set; } 
        public int PLA_Id { get; set; }
    }
}
