using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ParkingBO
    {
        public string PARK_Nom { get; set; } = string.Empty;
        public int PARK_NbEtages { get; set; } = 0;
        public int PARK_NbPlaces { get; set; } = 0;
        public int ADRS_Numero { get; set; } = 0;
        public string ADRS_NomRue { get; set; } = string.Empty;
        public string ADRS_Ville { get; set; } = string.Empty;
        public decimal ADRS_Latitude { get; set; } = 0;
        public decimal ADRS_Longitude { get; set; } = 0;
        public int PAYS_Id { get; set; }
    }
}
