using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Personne
    {
        public int PERS_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; } = string.Empty;
        public string PERS_Password { get; set; } = string.Empty;
        
         

        public virtual Client? Client { get; set; }

        public virtual Employee? Employee { get; set; }

        public List<PersonneRole> PersonneRoles { get; set; } = [];
    }
}
