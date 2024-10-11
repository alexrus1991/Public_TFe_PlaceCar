namespace PlaceCar.API.Models
{
    public class ReadParkingDTO
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; }
        public int PARK_NbEtages { get; set; }
        public int PARK_NbPlaces { get; set; }
    }
}
