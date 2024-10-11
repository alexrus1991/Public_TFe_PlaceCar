namespace PlaceCar.API.Models
{
    public class ReadTransacDTO
    {
        public int TRANS_Id { get; set; }
        public decimal TRANS_Somme { get; set; }
        public DateTime TRANS_Date { get; set; }
        public string TRANS_Communication { get; set; }
        // public int CB_Id { get; set; }

        public string CB_Nom { get; set; }

        public string CB_NumCompte { get; set; }
        public string Nom { get; set; } 

    }
}

