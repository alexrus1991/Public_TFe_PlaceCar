using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Adresse
    {
        public int ADRS_Id { get; set; }
        public int ADRS_Numero { get; set; } = 0;
        public string ADRS_NomRue { get; set; } = string.Empty;
        public string ADRS_Ville { get; set; } = string.Empty;
        public decimal ADRS_Latitude { get; set; } = 0;
        public decimal ADRS_Longitude { get; set; } = 0;
      //  public int ParkingId { get; set; }
        public ParkingEntity? Parking { get; set; }
        public int PaysId { get; set; }
        public PaysEntity? Pays { get; set; }
    }
}
