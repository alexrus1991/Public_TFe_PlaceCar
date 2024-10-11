using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Preferences
    {
        public int PlaceId { get; set; }

        public int ParkingId { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;

        public virtual ParkingEntity Parking { get; set; } = null!;

        public virtual PlaceParking Place { get; set; } = null!;
    }
}
