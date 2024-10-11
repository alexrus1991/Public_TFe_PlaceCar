﻿namespace PlaceCar.API.Models
{
    public class parkingDto
    {
        public string PARK_Nom { get; set; } = string.Empty;
        public int PARK_NbEtages { get; set; } = 0;
        public int PARK_NbPlaces { get; set; } = 0;
        public int ADRS_Numero { get; set; } = 0;
        public string ADRS_NomRue { get; set; } = string.Empty;
        public string ADRS_Ville { get; set; } = string.Empty;
        public decimal ADRS_Latitude { get; set; } = 0;
        public decimal ADRS_Longitude { get; set; } = 0;
        public int PAYS_Id { get; set; }
    }
}
