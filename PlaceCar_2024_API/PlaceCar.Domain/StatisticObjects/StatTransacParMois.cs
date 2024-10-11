using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.StatisticObjects
{
    public class StatTransacParMois
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TransactionNombre { get; set; }
        public decimal TotalSomme { get; set; }
    }
}
