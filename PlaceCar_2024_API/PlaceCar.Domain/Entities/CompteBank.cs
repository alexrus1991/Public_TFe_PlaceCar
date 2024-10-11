using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PlaceCar.Domain.Entities
{
    public class CompteBank
    {
        public int CB_Id { get; set; }

        public string CB_Nom { get; set; } 

        public string CB_NumCompte { get; set; } 

        public DateTime CB_Date { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<Trensaction> Transactions { get; set; } = new List<Trensaction>();
    }
}
