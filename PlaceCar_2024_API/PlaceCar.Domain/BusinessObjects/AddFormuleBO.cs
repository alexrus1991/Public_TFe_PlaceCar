using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddFormuleBO
    {
        public decimal Prix { get; set; }

        public int TypeId { get; set; }

        public int ParkingId { get; set; }
    }
}
