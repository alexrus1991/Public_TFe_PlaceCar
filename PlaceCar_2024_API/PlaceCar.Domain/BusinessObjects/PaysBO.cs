using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class PaysBO
    {
        public int PAYS_Id { get; set; }
        public string PAYS_Nom { get; set; }
        IEnumerable<Adresse> Adresses { get; set; } = [];
        //public int ADRS_Id { get; set; }
        //public int ADRS_Numero { get; set; } 
        //public string ADRS_NomRue { get; set; } 
        //public string ADRS_Ville { get; set; } 
        //public decimal ADRS_Latitude { get; set; } 
        //public decimal ADRS_Longitude { get; set; }
        //public int PARK_Id { get; set; }
        //public string PARK_Nom { get; set; } 
        //public int PARK_NbEtages { get; set; } 
        //public int PARK_NbPlaces { get; set; } 
    }
}
