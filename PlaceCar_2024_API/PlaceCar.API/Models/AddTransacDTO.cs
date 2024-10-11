namespace PlaceCar.API.Models
{
    public class AddTransacDTO
    {
        public string TRANS_Communication { get; set; }
        public int FactureId { get; set; }
        public decimal Somme { get; set; }
        public int CompteUnId { get; set; }
        public string Cb_NumCompte_Client { get; set; }
        public bool Preference { get; set; }
    }
}
