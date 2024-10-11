namespace PlaceCar.API.Models
{
    public class ReadDeataiTransacDTO
    {
        public int TransactionId { get; set; }
        public decimal TransactionSomme { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionCommunication { get; set; }
        public int ClientId { get; set; }
        public string ClientPrenom { get; set; }
        public string ClientNom { get; set; }
        public int ClientComptetId { get; set; }
        public string ClientComptetNumero { get; set; }
        public string BeneficierNom { get; set; }
        public string BeneficiereCompteNumero { get; set; }
        public int ParkingId { get; set; }
        public string ParkingNom { get; set; }
    }
}
