using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Trensaction
    {
        public int TRANS_Id { get; set; }
        public decimal TRANS_Somme { get; set; } 
        public DateTime TRANS_Date { get; set; }
        public string TRANS_Communication { get; set; } 

        public int FactureId { get; set; }
        public Facture? Facture { get; set; }

        public int CompteUnId { get; set; }
        public CompteBank? CompteUn { get; set; }

        public string CompteEntreprise { get; set; } 

        public virtual InfoEntreprise CompteEntrepriseNavigation { get; set; } 

    }
}
