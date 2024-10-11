using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class ReservationNotFoundExeption : Exception
    {
        public ReservationNotFoundExeption(int resId) : base($"La Reservation avec identifient {resId} n'est pas trouvée")
        {
        }
    }
}
