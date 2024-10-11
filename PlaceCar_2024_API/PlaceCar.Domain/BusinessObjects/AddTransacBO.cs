using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class AddTransacBO
    {
        public string TRANS_Communication { get; set; }
        public int FactureId { get; set; }
        public decimal Somme {  get; set; }
        public int CompteUnId { get; set; }
        public string Cb_NumCompte_Client { get; set; }
        public bool Preference { get; set; }
    }
}
