using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddResBO
    {
        public DateTime RES_DateDebut { get; set; }
        public DateTime? RES_DateFin { get; set; }

        public int PlaceId { get; set; }

        public int ClientId { get; set; }
        public int? FormPrixId { get; set; }
    }
}
