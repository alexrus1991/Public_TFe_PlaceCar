using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class FormulePrixReservationExeption : Exception
    {
        public FormulePrixReservationExeption() : base($"Vous pouvez prendre que les formules supperieures en delais !!")
        {
        }
    }
}
