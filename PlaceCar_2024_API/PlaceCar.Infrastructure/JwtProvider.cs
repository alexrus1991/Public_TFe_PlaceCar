using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlaceCar.Application.Interfaces.Provider;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)// Le constructeur extrait la valeur des options de configuration
        {
            _options = options.Value;
        }

        public IOptions<JwtOptions> Options { get; }//propriété en lecture,
                                                    //permet à d'autres classes d'accéder aux options configurées sans avoir besoin d'injecter directement IOptions<JwtOptions>
        public string CreateToken(Personne personne) //methode principale
        {
            var expiration = DateTime.Now.AddMinutes(_options.ExpiresMinutes);
            var token = CreateJwtToken(//créer un objet token avec les infos fournis
                CreateClaims(personne),
                CreateSigningCredentials(),//créer les informations de signature
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler(); // utilise un JwtSecurityTokenHandler pour écrire le token en chaîne de caractères 

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,//créer un objet token avec les infos fournis
            DateTime expiration) =>
            new(
                _options.ValidIssuer,
                _options.ValidAudience, 
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(Personne personne) //décrire l'entité (sujet) du jeton JWT
        {
            //var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["JwtRegisteredClaimNamesSub"];

            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _options.JwtRegisteredClaimNamesSub),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, personne.PERS_Id.ToString()),
                    new Claim(ClaimTypes.Name, personne.PERS_Nom),
                    new Claim(ClaimTypes.Email, personne.PERS_Email)
                
            };

                foreach (PersonneRole Pr in personne.PersonneRoles) {
                    claims.Add(new Claim(ClaimTypes.Role, Pr.Role.Role_Name));
                }

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()//utilisée pour créer les informations de signature (SigningCredentials) nécessaires pour signer le token JWT
        {
            
            return new SigningCredentials(
                new SymmetricSecurityKey(//créer un objet SymmetricSecurityKey, qui est utilisé comme clé de signature
                    Encoding.UTF8.GetBytes(_options.SecretKey)
                ),
                SecurityAlgorithms.HmacSha256//algorithme de signature HMACSHA256 
            );
        }

        public async Task RevokeTokenAsync(string token)
        {
            await RevokeTokenAsync(token);
        }

        public async Task<bool> IsTokenRevokedAsync(string token)
        {
            return await IsTokenRevokedAsync(token);
        }
    }
}
