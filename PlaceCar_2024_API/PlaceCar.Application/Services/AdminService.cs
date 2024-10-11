using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using PlaceCar.Domain.Exceptions.Business;
using PlaceCar.Domain.StatisticObjects;

namespace PlaceCar.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        public async Task AddNewPays(string nomPays)
        {

            try
            {
                bool ok = await unitOfWork.Pays.PaysExiste(nomPays);
                if (nomPays == null) { throw new InvalidOperationException("Le Nom du paays est vide !!!"); }
                else if (ok == true) { throw new PaysDoublonExeption(nomPays); }
                else
                {
                    var pays = new PaysEntity { PAYS_Nom = nomPays };
                    await unitOfWork.Pays.AddPays(pays);
                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async Task AddEmplyee(AddEmpBO empBO,bool Admin = false)
        //{
        //    try
        //    {
        //        var personne = mapper2.Map<Personne>(empBO);
        //        var parking = await unitOfWork.Parking.GetParkingById(empBO.ParkingId);

        //        if (parking == null) { throw new ArgumentException("Le parking spécifié n'existe pas!!"); }
        //        else
        //        {
        //            var emp = new Employee { EmpPers = personne, IsAdmin = Admin };

        //            await unitOfWork.Personne.AddPersonne(personne);
        //            await unitOfWork.Employe.AddEmployee(emp, parking);

        //            await unitOfWork.SaveAsync();
        //        }
                
        //    }
        //    catch(Exception ex) { throw; }
            
        //}

        public async Task AddParking(ParkingBO parkingBO)
        {
            try
            {
                var adresse = mapper2.Map<Adresse>(parkingBO);
                var parking = mapper2.Map<ParkingEntity>(parkingBO);

                await unitOfWork.Parking.AddParking(parking);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmpWorkOnBO>> GetAllEmpWorkOnBOs(int parkingId)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                List<EmpWorkOnBO> res = new List<EmpWorkOnBO>();
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                else
                {
                    var ewo = await unitOfWork.EmpWorkOn.GetAllEmpInParking(parkingId);
                    if (ewo.Count > 0 ) 
                    {
                        res = mapper2.Map<List<EmpWorkOnBO>>(ewo);
                    }
                   
                    await unitOfWork.SaveAsync();
                }
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<EmpWorkOnBO> UpdateEmployeeWorkOnParking(int employeeId, int parkingId)
        {
            try
            {
                EmpWorkOnBO ewoBo = new EmpWorkOnBO();
                var employee = await unitOfWork.Employe.GetEmployeeById(employeeId);
                var empworkon = await unitOfWork.EmpWorkOn.GetEmployeWorkOn(employeeId);
                var park = await unitOfWork.Parking.GetParkingById(parkingId);

                if(employee == null) { throw new EmployeeNotFoundExeption(employeeId); }
                if(empworkon == null && employee.Emp_Pers_Id != empworkon.Emp_Pers_Id) { throw new ArgumentNullException("Emplyée demandé ne travaille pas dans meme parking !!"); }
                if(park == null || park.PARK_Id == empworkon.ParkingId) { throw new ArgumentNullException("Emplyée travaille déjà dans le parking specifié !!"); }
                else
                {
                    empworkon.ParkingId = park.PARK_Id;
                    await unitOfWork.SaveAsync();
                }
                var e = await unitOfWork.EmpWorkOn.UpdateParkingEmpWork(empworkon);
                var reponce = await unitOfWork.EmpWorkOn.GetEmpWorkOnByEmployeeId(e.Emp_Pers_Id);
                ewoBo = mapper2.Map<EmpWorkOnBO>(reponce);
                return ewoBo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteEmpWorkOn(int employeeId, int parkingId)
        {
            try
            {
                var employee = await unitOfWork.Employe.GetEmployeeById(employeeId);
                var empworkon = await unitOfWork.EmpWorkOn.GetEmployeWorkOn(employeeId);
                var park = await unitOfWork.Parking.GetParkingById(parkingId);

                if (empworkon == null || employee.Emp_Pers_Id != empworkon.Emp_Pers_Id) { throw new ArgumentNullException("Emplyée demandé ne travaille pas dans meme parking !!"); }
                if (park == null || park.PARK_Id == empworkon.ParkingId) { throw new ArgumentNullException("Emplyée travaille déjà dans le parking specifié !!"); }
                else
                {
                    await unitOfWork.EmpWorkOn.DeleteEmployeeWorkOn(employee.Emp_Pers_Id,park.PARK_Id);
                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteEmployéé(int employeeId)
        {
            try
            {
                var employee = await unitOfWork.Employe.GetEmployeeById(employeeId);
                var empworkon = await unitOfWork.EmpWorkOn.GetEmployeWorkOn(employeeId);
                var client = await unitOfWork.Client.GetClientPersonneById(employee.Emp_Pers_Id);
                if (empworkon == null) { throw new NotWorkingParkingException(employee.EmpPers); }// new ArgumentNullException("Emplyée ne travaille dans aucun parking !!"); }
                if (employee == null) { throw new EmployeeNotFoundExeption(employeeId); }
                else
                {
                    await unitOfWork.EmpWorkOn.DeleteEmployeeWorkOn(employee.Emp_Pers_Id,empworkon.ParkingId);
                    await unitOfWork.Employe.DeleteEmployee(employee.EmpPers.PERS_Id);
                    if (client == null)
                    {
                        await unitOfWork.Personne.DeletePersonne(employee.Emp_Pers_Id);
                    }

                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<EmpWorkOnBO>> GetAllEmplyeesWorkIn()
        {
            try
            {
                List<EmpWorkOnBO> res = new List<EmpWorkOnBO>();
                //var role = await unitOfWork.Role.GetRoleById((await unitOfWork.Role.GetRoles()).First(r => r.Role_Name == RoleEnum.Admin.ToString()).Role_Id);

                // if (userId == null) { throw new ArgumentNullException("Utilisateur Id est vide !!"); }
                //var pr = await unitOfWork.PersonneRole.GetPersonneRole(userId, role.Role_Id);
                //if(userId != pr.Personne.PERS_Id && role.Role_Id != pr.Role.Role_Id) { throw new ArgumentNullException("Utilisateur Id ne possede pas le Role Admin !!"); }
                //else
                //{
                //    var ewo = await unitOfWork.EmpWorkOn.GetAllEmplyeesInGroup();
                //    if (ewo.Count > 0)
                //    {
                //        res = mapper2.Map<List<EmpWorkOnBO>>(ewo);
                //    }

                //    await unitOfWork.SaveAsync();
                //}

                var ewo = await unitOfWork.EmpWorkOn.GetAllEmplyeesInGroup();
                if (ewo.Count > 0)
                {
                    res = mapper2.Map<List<EmpWorkOnBO>>(ewo);
                }

                await unitOfWork.SaveAsync();
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> GetEmployeeNombre()
        {
            return await unitOfWork.Employe.GetEmployeeNombreTotal();
        }

        public async Task<int> GetEmployeeNombreByParking(int parkingId)
        {
            try
            {
                var prk = await unitOfWork.Parking.GetParkingById(parkingId);
                if(prk == null) { throw new ParkingNotFoundExeption(parkingId); }
                else
                {
                    return await unitOfWork.EmpWorkOn.GetEmployeeNombreInParking(parkingId);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<List<StatReservationParMois>> GetResStatParMois()
        {
            try
            {
                List<StatReservationParMois> lstResStat = await unitOfWork.Reservation.GetStatReservation();
                if(lstResStat == null) { throw new ArgumentNullException("Liste des Statistics de reservation est vide !!"); }
                else { return lstResStat; } 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<StatTransacParMois>> GetTransStatParMois()
        {
            try
            {
                List<StatTransacParMois> lstTransStat = await unitOfWork.Trensaction.GetStatTransaction();
                if (lstTransStat == null) { throw new ArgumentNullException("Liste des Statistics de transaction est vide !!"); }
                else { return lstTransStat; }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetNombreTotalParkings()
        {
            try
            {            
                int nb = await unitOfWork.Parking.GetNombreParkingsPlaceCar();
                if (nb == 0) { throw new ArgumentException("Pas de Parkings !!"); }
                return nb;
                
            }
            catch (Exception)
            {

                throw;
            }
        }



        //public async Task<List<EmpWorkOnBO>> GetAllEmplyees(int userId)
        //{
        //    try
        //    {
        //        List<EmpWorkOnBO> res = new List<EmpWorkOnBO>();
        //        var role = await unitOfWork.Role.GetRoleById((await unitOfWork.Role.GetRoles()).First(r => r.Role_Name == RoleEnum.Admin.ToString()).Role_Id);

        //        if (userId == null) { throw new ArgumentNullException("Utilisateur Id est vide !!"); }
        //        var pr = await unitOfWork.PersonneRole.GetPersonneRole(userId, role.Role_Id);
        //        if (userId != pr.Personne.PERS_Id && role.Role_Id != pr.Role.Role_Id) { throw new ArgumentNullException("Utilisateur Id ne possede pas le Role Admin !!"); }
        //        else
        //        {
        //            var emp = await unitOfWork.Employe.GetAllEmplyees();
        //            if (emp.Count > 0)
        //            {
        //                res = mapper2.Map<List<EmpWorkOnBO>>(emp);
        //            }

        //            await unitOfWork.SaveAsync();
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
    
}
