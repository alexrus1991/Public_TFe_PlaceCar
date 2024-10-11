using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class PlaceParking
    {
        public int PLA_Id { get; set; }
        public int PLA_Etage { get; set; } = 0;
        public int PLA_NumeroPlace { get; set; } = 0;


        public int ParkingId { get; set; }
        public ParkingEntity? Parking { get; set; }


       // public int? ReservationId { get; set; }
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();

        public List<Preferences> Preferences { get; set; } = [];
    }
}
