using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddPrefBO
    {
        public int PlaceId { get; set; }

        public int ParkingId { get; set; }

        public int ClientId { get; set; }
    }
}
