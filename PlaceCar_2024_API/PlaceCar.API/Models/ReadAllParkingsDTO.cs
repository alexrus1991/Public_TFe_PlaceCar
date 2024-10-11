namespace PlaceCar.API.Models
{
    public class ReadAllParkingsDTO
    {     
            public int PARK_Id { get; set; }
            public string PARK_Nom { get; set; }
            public int PARK_NbEtages { get; set; }
            public int PARK_NbPlaces { get; set; }
            public string ADRS_Ville { get; set; }
            public string PAYS_Nom { get; set; }
    }
}
