using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Facture
    {
        public int FACT_Id { get; set; }
        public decimal FACT_Somme { get; set; } 
        public DateTime FACT_Date { get; set; }

        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public int TransactionId { get; set; }
        public Trensaction? Trensaction { get; set; }

        public string? StripeConfirmStr { get; set; }
        public bool? Status { get; set; }
    }
}
