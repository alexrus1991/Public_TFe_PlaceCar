namespace PlaceCar.API.Models
{
    public class AddClientDto
    {
        public string PERS_Nom { get; set; }
        public string PERS_Prenom { get; set; }
        public DateTime PERS_DateNaissance { get; set; }
        public string PERS_Email { get; set; }
        public string PERS_Password { get; set; }
        //public int RoleId { get; set; } ==> On doit lui attribuer le rôle de manière automatique lors de l'enregistrement
        //Tu devras probablement ajouter une propriété pour savoir si tu créé un employee ou un client ;)

        
    }
}
