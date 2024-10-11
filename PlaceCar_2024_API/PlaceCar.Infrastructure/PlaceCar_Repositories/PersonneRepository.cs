using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure.PlaceCar_Repositories
{
    public class PersonneRepository : IPersonneRepository
    {
        private readonly PlaceCarDbContext _context;

        public PersonneRepository(PlaceCarDbContext context)
        {
            _context = context;
        }

        public async Task AddPersonne(Personne personne, Role role)
        {
            if (personne == null && role == null)
            {

                throw new ArgumentNullException("Le paramètre personne ou le paramètre rôle ne contiennent pas d'éléments. Le rôle ne peut être associé à une personne dans la table PersonneRole.");
            }
            else
            {
                _context.Personne.Add(personne);
                _context.PersonneRole.Add(new PersonneRole() { Personne = personne, Role = role });
                _context.Entry<Role>(role).State = EntityState.Unchanged;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Personne> GetPersonneByEmail(string email)
        {
           try
            {
                Personne personne = new Personne();
                if (email != " ")
                {
                    personne = await _context.Personne
                      .AsNoTracking()
                      .Include(p => p.PersonneRoles)
                      .ThenInclude(pr => pr.Role)
                      .FirstOrDefaultAsync(p => p.PERS_Email == email) ?? throw new Exception("Votre email est incorrect où vous devez d'abord vous enregistrer pour confirmer cette réservation");
                }
                return personne;
           }
            catch (Exception ex)
           {
                //Type typeException = ex.GetType();
                //string nomException = typeException.Name;


                //Console.WriteLine($"Exception : {nomException}");
                throw ;
            }
        }

        public async Task<Personne> GetPersonneById(int clientId)
        {
            var personne = await _context.Personne
             .AsNoTracking()
             .Include(p => p.Client)
             .ThenInclude(c => c.Comptes) // Inclure les comptes associés au client
             .Include(p => p.Employee) // Inclure l'employé associé à la personne
             .FirstOrDefaultAsync(p => p.Client != null && p.Client.Cli_Id == clientId);

            return personne;
        }

        public async Task<bool> UpdatePersonneInfo(int id, string pwd)
        {
            try
            {
                int reponse = _context.Personne
                        .Where(c => c.PERS_Id == id)
                        .ExecuteUpdate(c => c
                                //.SetProperty(c => c.Cli.PERS_Email, mail)
                                .SetProperty(c => c.PERS_Password, pwd));

                if (reponse > 0) { return true; }
                else
                {
                    throw new InvalidOperationException("Client does not exist");
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeletePersonne(int personId)
        {
            try
            {
                var personne = await _context.Personne
                  .FirstOrDefaultAsync(p =>p.PERS_Id  == personId);

                if (personne == null) { throw new InvalidOperationException("La Personne n'a pas été trouvé."); }
                else
                {
                    _context.Personne.Remove(personne);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
