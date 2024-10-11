using PlaceCar.Application.Interfaces.PwdHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);//Genere le pwd Hashé
        
        
        public bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);//verifie que pwd est bie Hashé et retourne le resulta de verification
                                                                       //Lorsqu’on utilisateur se logue on vérifie si le mot de passe entrez par utilisateur correspond à celui qui a été haché
                                                                       //et qui se trouve dans la base de données.
    }
}
