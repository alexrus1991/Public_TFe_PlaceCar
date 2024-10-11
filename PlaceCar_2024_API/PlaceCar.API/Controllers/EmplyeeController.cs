using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.StatisticObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public EmplyeeController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        //[HttpPost("Parking/{ParkingId:int}/NouveauEmployée")]
        //public async Task<ActionResult> CreateEmplyee([FromBody] AddEmplDto emplDto)
        //{
        //    try
        //    {
        //        if (emplDto != null)
        //        {
        //            var employee = _mapper.Map<AddEmpBO>(emplDto);
        //            await _adminService.AddEmplyee(employee);

        //            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
        //            string uri = $"{baseUrl}?parkId={emplDto.ParkingId}&clientId={emplDto.PERS_Nom}'";

        //            return Created(uri,employee);
        //        }
        //        else 
        //        { 
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex) 
        //    { 

        //        throw;     
        //    }

        //}

        //[HttpPost("NouveauAdmin")]
        //public async Task<ActionResult> CreateAdmin([FromBody] AddEmplDto emplDto)
        //{
        //    try
        //    {
        //        if (emplDto != null)
        //        {
        //            var employee = _mapper.Map<AddEmpBO>(emplDto);
        //            await _adminService.AddEmplyee(employee,true);

        //            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
        //            string uri = $"{baseUrl}?parkId={emplDto.ParkingId}&clientId={emplDto.PERS_Nom}'";

        //            return Created(uri, employee);
        //        }
        //        else { return BadRequest(); }
        //    }
        //    catch (Exception ex) { throw; }

        //}

        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet("Parking/{parkingId:int}")]
        public async Task<ActionResult<List<ReadEmpWorkInDTO>>> GetAllEmpInParking(int parkingId)
        {
            try
            {
                var listEmp = await _adminService.GetAllEmpWorkOnBOs(parkingId);
                return Ok(_mapper.Map<List<ReadEmpWorkInDTO>>(listEmp));
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Pour le moment aucun employé travaille dans ce parking");
            }
        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("DuGroupe")]
        public async Task<ActionResult<List<ReadEmpWorkInDTO>>> GetAllEmplyeesWorkOn()
        {
            try
            {
                var listEmp = await _adminService.GetAllEmplyeesWorkIn();
                return Ok(_mapper.Map<List<ReadEmpWorkInDTO>>(listEmp));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<List<ReadEmpWorkInDTO>>> GetAllEmplyees(int userId)
        //{
        //    try
        //    {
        //        var listEmp = await _adminService.GetAllEmplyees(userId);
        //        return Ok(_mapper.Map<List<ReadEmpWorkInDTO>>(listEmp));
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("Parking/{parkingId:int}/Changement")]//("Parking/{ParkingId:int}/Changement")
        public async Task<IActionResult> UpdateEmployeeDeParking(int employeeId, int parkingId)
        {
            try
            {
                var reponce = await _adminService.UpdateEmployeeWorkOnParking(employeeId, parkingId);
                var rp = _mapper.Map<ReadEmpWorkInDTO>(reponce);
                if (reponce == null)
                {
                    return NotFound(new { rp.Emp_Pers_Id, rp.PARK_Id });
                }
                else
                {

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le changement de place de travail de l'employé n'a pas pu être effectué!");
            }
        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("Employee/{employeeId:int}/")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                await _adminService.DeleteEmployéé(employeeId);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"L'employé n'a pas pu être supprimé !");
            }
        }

        [HttpGet("Count-NombreTotal-Employees")]
        public async Task<ActionResult<int>> GetEmployeesNombre()
        {
            int count = await _adminService.GetEmployeeNombre();
            return Ok(count);
        }

        [HttpGet("Count-NombreTotal-Employees-Parking")]
        public async Task<ActionResult<int>> GetEmployeesNombreParking(int parkingId)
        {
            int count = await _adminService.GetEmployeeNombreByParking(parkingId);
            return Ok(count);
        }

        [HttpGet("Count-NombreTotal--Parkings")]
        public async Task<ActionResult<int>> GetNombreParking()
        {
            int count = await _adminService.GetNombreTotalParkings();
            return Ok(count);
        }

        [HttpGet("Stat-Reservation_Mois")]
        public async Task<ActionResult<List<StatReservationParMois>>> GetAdminStatReservationsMois()
        {
            try
            {
                List<StatReservationParMois> lstRes = await _adminService.GetResStatParMois();
                return Ok(lstRes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("Stat-Transaction_Mois")]
        public async Task<ActionResult<List<StatTransacParMois>>> GetAdminStatTransactionsMois()
        {
            try
            {
                List<StatTransacParMois> lstRes = await _adminService.GetTransStatParMois();
                return Ok(lstRes);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
