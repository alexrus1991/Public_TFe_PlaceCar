using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadClientBo
    {
        public int Cli_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; } = string.Empty;
    }
}
