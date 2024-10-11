using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class ClientReservationExeption : Exception
    {
        public ClientReservationExeption() : base($"La reservation  n'appartient pas au client !!") 
        {
        }
    }
}
