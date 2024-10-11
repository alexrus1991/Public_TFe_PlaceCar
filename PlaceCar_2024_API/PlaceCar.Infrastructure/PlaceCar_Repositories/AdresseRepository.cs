using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class AdresseRepository : IAdresseRepository
    {
        private readonly PlaceCarDbContext _context;

        public AdresseRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddAdresse(Adresse adresse)
        {
            
            try
            {
                if(adresse != null)
                {
                    bool reponce = AdresseExists(adresse.ADRS_Numero, adresse.ADRS_NomRue, adresse.ADRS_Ville);
                    if (reponce == false)
                    {
                        _context.Adresse.Add(adresse);
                    }
                    else { throw new ArgumentException("L'adresse spécifié existe déja."); }
                }
                else { throw new ArgumentNullException(nameof(adresse)); }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AdresseExists(int numero, string nomRue, string ville)
        {
            // Vérifie si une adresse existe déjà dans la base de données en fonction du numéro de rue, du nom de rue et de la ville
            return _context.Adresse.Any(a => a.ADRS_Numero == numero &&
                                                a.ADRS_NomRue == nomRue &&
                                                a.ADRS_Ville == ville);
        }
    }
}
