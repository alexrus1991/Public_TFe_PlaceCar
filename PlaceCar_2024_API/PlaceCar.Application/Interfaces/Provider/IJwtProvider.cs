using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Provider
{
    public interface IJwtProvider
    {
        string CreateToken(Personne personne);
        Task RevokeTokenAsync(string token);
        Task<bool> IsTokenRevokedAsync(string token);
    }
}
