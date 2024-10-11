namespace PlaceCar.API.Models
{
    public class ReadClientDto
    {
        public int Cli_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; } = string.Empty;
    }
}
