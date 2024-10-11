using PlaceCar.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPaysRepository Pays { get; }
        IParkingRepository Parking { get; }
        IClientRepository Client { get; }
        IPersonneRepository Personne { get; }
        IReservationRepository Reservation { get; }
        IFormuleRepository Formule { get; }
        IFormuleTypeRepository FormuleType { get; }
        IPlacesRepository Place { get; }
        ICompteRepository Compte { get; }
        IEmployeRepository Employe { get; }
        IAdresseRepository Adresse { get; }
        IFactureRepository Facture { get; }
        IEmpWorkOnRepository EmpWorkOn { get; }
        ITransactionRepository Trensaction { get; }
        IInfoEntrpriseRepository InfoEntrprise { get; }
        IPreferancesRepository Preferances { get; }
        IRoleRepository Role { get; }
        IPersonneRoleRepository PersonneRole { get; }
        Task<int> SaveAsync();
    }
}
