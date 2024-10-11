namespace PlaceCar.API.Models
{
    public class ReadEmpWorkInDTO
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; } = string.Empty;
        public int Emp_Pers_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; } = string.Empty;
    }
}
