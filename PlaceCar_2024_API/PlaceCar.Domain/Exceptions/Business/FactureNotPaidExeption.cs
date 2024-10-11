using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class FactureNotPaidExeption : Exception
    {
        public FactureNotPaidExeption() : base($"Impossible de reserver si une facture n'est pas réglée !!")
        {
        }
    }
}
