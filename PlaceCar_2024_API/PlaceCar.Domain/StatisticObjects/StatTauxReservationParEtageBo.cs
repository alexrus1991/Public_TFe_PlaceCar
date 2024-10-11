using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.StatisticObjects
{
    public class StatTauxReservationParEtageBo
    {
        public int PlacesReservees { get; set; }
        public int PlacesNonReservees { get; set; }
        public double PourcentagePlacesReservees { get; set; }
        public double PourcentagePlacesNonReservees { get; set; }
    }
}
