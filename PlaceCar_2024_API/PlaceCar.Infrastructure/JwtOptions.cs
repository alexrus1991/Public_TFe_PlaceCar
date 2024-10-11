using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure
{
    public class JwtOptions//stocker les options de configuration liées à la génération et à la validation des jetons JWT
    {
        public string SecretKey { get; set; } = string.Empty;//utilisée pour signer les jetons JWT, vérifier l'intégrité des token et génére des signatures sécurisées
        public int ExpiresMinutes {  get; set; }

        public string ValidIssuer { get; set; }//qui a émis le token JWT.
        public string ValidAudience { get; set; }//destinataires pour lesquels le token JWT est destiné
        public string JwtRegisteredClaimNamesSub { get; set; }//(claims) du token JWT,"sub "est un claim standard qui représente le sujet du token


    }
}
