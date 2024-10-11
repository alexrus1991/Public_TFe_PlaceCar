namespace PlaceCar.API.Models
{
    public class ReadParkVilleDTO
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; }
        public int PARK_NbEtages { get; set; }
        public int PARK_NbPlaces { get; set; }
        public int ADRS_Id { get; set; }
        public int ADRS_Numero { get; set; }
        public string ADRS_NomRue { get; set; }
        public string ADRS_Ville { get; set; }
        public decimal ADRS_Latitude { get; set; }
        public decimal ADRS_Longitude { get; set; }
        public string PAYS_Nom { get; set; }
    }
}
