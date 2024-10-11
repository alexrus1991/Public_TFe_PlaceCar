using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Infrastructure.PlaceCar_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly PlaceCarDbContext _context;
        private IPaysRepository _paysRepo;
        private IParkingRepository _parkingRepo;
        private IPersonneRepository _personneRepo;
        private IClientRepository _clientRepo;
        private IReservationRepository _reservationRepo;
        private IFormuleRepository _formuleRepo;
        private IFormuleTypeRepository _formuleTypeRepo;
        private IPlacesRepository _placesRepo;
        private ICompteRepository _compteRepo;
        private IEmployeRepository _employeRepo;
        private IAdresseRepository _adresseRepo;
        private IFactureRepository _factureRepo;
        private IEmpWorkOnRepository _empworkOnRepo;
        private ITransactionRepository _transactionRepo;
        private IInfoEntrpriseRepository _infoEntrpriseRepo;
        private IPreferancesRepository _preferancesRepo;
        private IRoleRepository _roleRepo;
        private IPersonneRoleRepository _personneRoleRepo;
        public UnitOfWork(PlaceCarDbContext context)
        {
            _context = context;
        }

        //public IPaysRepository Pays => new PaysRepository(_context);
        public IPaysRepository Pays
        {
            get { return _paysRepo = _paysRepo ?? new PaysRepository(_context); }
        }

        public IParkingRepository Parking
        {
            get { return _parkingRepo = _parkingRepo ?? new ParkingRepository(_context); }
        }
        public IPersonneRepository Personne
        {
            get { return _personneRepo = _personneRepo ?? new PersonneRepository(_context); }
        }
        public IClientRepository Client
        {
            get { return _clientRepo = _clientRepo ?? new ClientRepository(_context); }
        }
        public IReservationRepository Reservation
        {
            get { return _reservationRepo = _reservationRepo ?? new ReservationRepository(_context); }
        }

        public IFormuleRepository Formule
        {
            get { return _formuleRepo = _formuleRepo ?? new FormuleRepository(_context); }
        }

        public IFormuleTypeRepository FormuleType
        {
            get { return _formuleTypeRepo = _formuleTypeRepo ?? new FormuleTypeRepository(_context); }
        }

        public IPlacesRepository Place
        {
            get { return _placesRepo = _placesRepo ?? new PlacesRepository(_context); }
        }

        public ICompteRepository Compte
        {
            get { return _compteRepo = _compteRepo ?? new CompteRepository(_context); }
        }

        public IEmployeRepository Employe
        {
            get { return _employeRepo = _employeRepo ?? new EmployeeRepository(_context); }
        }

        public IAdresseRepository Adresse
        {
            get { return _adresseRepo = _adresseRepo ?? new AdresseRepository(_context); }
        }

        public IFactureRepository Facture 
        {
            get { return _factureRepo = _factureRepo ?? new FactureRepository(_context); }
        }

        public IEmpWorkOnRepository EmpWorkOn 
        { 
            get { return _empworkOnRepo = _empworkOnRepo ?? new EmpWorkOnRepository(_context); }
        }

        public ITransactionRepository Trensaction
        {
            get {return _transactionRepo = _transactionRepo ?? new TransactionRepository(_context); }
        }

        public IInfoEntrpriseRepository InfoEntrprise
        {
            get {return _infoEntrpriseRepo = _infoEntrpriseRepo ?? new InfoEntrpriseRepository(_context); }
        }
        public IPreferancesRepository Preferances
        {
            get { return _preferancesRepo = _preferancesRepo ?? new PreferancesRepository(_context); }
        }
        public IRoleRepository Role
        {
            get { return _roleRepo = _roleRepo ?? new RoleRepository(_context); }
        }
        public IPersonneRoleRepository PersonneRole
        {
            get { return _personneRoleRepo = _personneRoleRepo ?? new PersonneRoleRepository(_context); }
        }
        public void Dispose()
        {
           _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}
