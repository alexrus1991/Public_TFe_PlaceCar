using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class PlacePakingExeption : Exception
    {
        public PlacePakingExeption() : base($"Vous avez oublié de choisir une place de stationnement !!")
        {
        }
    }
}
