using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadAllParkingsBO
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; }
        public int PARK_NbEtages { get; set; }
        public int PARK_NbPlaces { get; set; }
        public string ADRS_Ville { get; set; }
        public string PAYS_Nom { get; set; }
    }
}
