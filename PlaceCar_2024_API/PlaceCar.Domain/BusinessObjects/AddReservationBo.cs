using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddReservationBo
    {
        public int ClientId { get; set; }
        public DateTime RES_DateDebut { get; set; }
        public DateTime RES_DateFin { get; set; }
        public decimal RES_DureeTotal_Initiale { get; set; } = 0;
        public decimal RES_DureeTotal_Reele { get; set; } = 0;
        public bool RES_Cloture { get; set; }
        public int PlaceId { get; set; }
        public int FormTypeId { get; set; }
    }
}
