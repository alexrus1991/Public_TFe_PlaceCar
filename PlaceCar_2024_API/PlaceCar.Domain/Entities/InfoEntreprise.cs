using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PlaceCar.Domain.Entities
{
    public class InfoEntreprise
    {
        public string Nom { get; set; } = null!;

        public string Cb_NumCompte { get; set; } = null!;

        public List<Trensaction> Transactions { get; set; } = [];
    }
}
