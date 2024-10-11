using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class PaysEntity
    {
        public int PAYS_Id { get; set; }
        public string PAYS_Nom { get; set; } = string.Empty;

        public List<Adresse> Adresses { get; set; } = [];
    }
}
