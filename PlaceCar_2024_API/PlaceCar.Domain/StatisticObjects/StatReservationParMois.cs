using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.StatisticObjects
{
    public class StatReservationParMois
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ReservationNombre { get; set; }
        public decimal TotalDuree { get; set; }
    }
}
