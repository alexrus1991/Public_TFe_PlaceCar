using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddCompteBo
    {
        public string CB_Nom { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
