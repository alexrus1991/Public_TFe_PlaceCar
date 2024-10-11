using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class ClientNotFoundExeption : Exception
    {
        public ClientNotFoundExeption() : base($"Client demandé n'est pas trouvé")
        {
        }
    }
}
