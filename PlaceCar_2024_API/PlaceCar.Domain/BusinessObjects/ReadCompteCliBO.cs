using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadCompteCliBO
    {
        public int CB_Id { get; set; }

        public string CB_Nom { get; set; }

        public string CB_NumCompte { get; set; }

        public DateTime CB_Date { get; set; }
        public int ClientId { get; set; }
        public string PERS_Nom { get; set; }
        public string PERS_Prenom { get; set; }
    }
}
