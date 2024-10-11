using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class PaysDoublonExeption : Exception
    {
        public PaysDoublonExeption(string nomPays) : base($"Le Pays {nomPays} existe déjé dans la leste des pays")
        {
        }
    }
}
