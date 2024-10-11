using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class NotWorkingParkingException : Exception
    {
        public NotWorkingParkingException(Personne pers): base($"{pers.PERS_Nom} ne travaille pas dans un parking")
        {
                
        }
    }
}
