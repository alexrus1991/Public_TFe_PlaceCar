using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadTransacBO
    {
        public int TRANS_Id { get; set; }
        public decimal TRANS_Somme { get; set; }
        public DateTime TRANS_Date { get; set; }
        public string TRANS_Communication { get; set; }
       // public int CB_Id { get; set; }

        public string CB_Nom { get; set; }

        public string CB_NumCompte { get; set; }
        public string Nom { get; set; }
    }
}
