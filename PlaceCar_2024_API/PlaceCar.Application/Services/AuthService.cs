using AutoMapper;
using PlaceCar.Application.Interfaces.Provider;
using PlaceCar.Application.Interfaces.PwdHasher;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PlaceCar.Application.Services
{
   
    
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public AuthService(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper2)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        public async Task AddClient(PersonneBo personne)
        {
            try
            {
                if (personne == null) { throw new ArgumentException("Le paramètre personne ne contient pas tous les éléments exigés pour créer un compte client"); }
                else
                {
                    var role = await unitOfWork.Role.GetRoleById((await unitOfWork.Role.GetRoles()).First(r=>r.Role_Name== RoleEnum.Client.ToString()).Role_Id);
                    if (role == null) { throw new ArgumentException("Le role spécifié n'existe pas!!"); }
                    var personn = mapper2.Map<Personne>(personne);
                    personn.PERS_Password = _passwordHasher.Generate(personn.PERS_Password);
                    var client = new Client { Cli = personn };

                    await unitOfWork.Personne.AddPersonne(personn,role);
                    await unitOfWork.Client.AddClient(client);
                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddEmplyee(AddEmpBO empBO, bool Admin = false)
        {
            try
            {
                //var personne = mapper2.Map<Personne>(empBO);
                var parking = await unitOfWork.Parking.GetParkingById(empBO.ParkingId);              
                var role = await unitOfWork.Role.GetRoleById((await unitOfWork.Role.GetRoles()).First(r => r.Role_Name ==( Admin?RoleEnum.Admin.ToString(): RoleEnum.Employee.ToString())).Role_Id);
                if(role == null) { throw new ArgumentException("Le role spécifié n'existe pas!!"); }
                if (parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas!!"); }
                else
                {
                    var personne = mapper2.Map<Personne>(empBO);
                    personne.PERS_Password = _passwordHasher.Generate(personne.PERS_Password);
                    var emp = new Employee { EmpPers = personne, IsAdmin = Admin };
                    await unitOfWork.Personne.AddPersonne(personne,role);
                    await unitOfWork.Employe.AddEmployee(emp, parking);

                    await unitOfWork.SaveAsync();
                }

            }
            catch (Exception ex) { throw; }
        }

        public async Task<List<Role>> GetRoles()
        {
            try
            {
                var lst = await unitOfWork.Role.GetRoles();
                if(lst.Count == 0) { throw new ArgumentException("Liste des Roles est vide !!"); }
                else { return lst; }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> Login(string email, string password)
        {
           // try
           // {
                var token = " ";
                var personne = await unitOfWork.Personne.GetPersonneByEmail(email);
                if(personne == null) { throw new ArgumentException("Le mot de passe est incorrect. Veuillez réessayer !!"); }
                else
                {
                    var result = _passwordHasher.Verify(password, personne.PERS_Password);

                    if (result == false) { throw new ArgumentException("Le mot de passe est incorrect. Veuillez réessayer !!"); }
                    else
                    {
                        token = _jwtProvider.CreateToken(personne);
                    }                 
                }
                await unitOfWork.SaveAsync();
                return token;
            /*}
            catch (Exception ex)
            {
                string test = ex.GetType().ToString();
                //throw new KeyNotFoundException($"Aucune personne trouvée avec l'email {email}.");
                throw;
            }*/
        }

        public async Task Logout(string token)
        {
            List<string> _revokedTokens = new List<string>();
            try
            {
                await _jwtProvider.RevokeTokenAsync(token);
                _revokedTokens.Add(token);
                await Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
