using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class FormuleDePrix
    {
        public int FORM_Id { get; set; }
        public decimal FORM_Prix { get; set; }

        public int TypeId { get; set; }
        public virtual FormuleDePrixType? FormuleDePrixType { get; set; }

        public int ParkingId { get; set; }
        public virtual ParkingEntity? Parking { get; set; }

        public List<Reservation> Reservations { get; set; } = [];

    }
}
