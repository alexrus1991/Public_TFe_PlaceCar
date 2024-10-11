using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Controllers
{
    [Route("api/Parkings")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        private readonly IMapper _mapper;

        public ParkingController(IParkingService parkingService, IMapper mapper)
        {
            this._parkingService = parkingService;
            _mapper = mapper;
        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> CreateParking(parkingDto parking)
        {
            try
            {
                if (parking != null)
                {
                    var parkingBo = _mapper.Map<ParkingBO>(parking);
                    await _parkingService.AddParking(parkingBo);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?parkingInfo={parking}";

                    return Created(uri, parking);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le parking n'a pas pu être créé!");
            }

        }

        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet("Pays/{paysId:int}/Ville/{nomVille}/")]
        public async Task<IActionResult> GetParkingsByVille(int paysId,string nomVille)
        {
            try
            {
                var res = await _parkingService.GetParkingsByPaysByVille(paysId, nomVille);
                List<ReadParkVilleDTO> resmap = _mapper.Map<List<ReadParkVilleDTO>>(res);

                return Ok(resmap);

            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucun parking n'est present dans le Pays {paysId} et laville {nomVille}!");

                //return NotFound($"Aucun parking n'est present dans le Pays {paysId} et laville {nomVille}");
            }
        }


        [HttpGet("Pays/{paysId:int}")]
        public async Task<IActionResult> GetParkingsByPays(int paysId)
        {
            try
            {
                var res = await _parkingService.GetParkingsByPays(paysId);
                List<ReadParkVilleDTO> resmap = _mapper.Map<List<ReadParkVilleDTO>>(res);

                return Ok(resmap);

            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucun parking n'est present dans le Pays {paysId}!");

                //return NotFound($"Aucun parking n'est present dans le Pays {paysId}");
            }
        }

        [HttpGet("Villes/Pays/{paysId:int}")]
        public async Task<IActionResult> GetVillesByPays(int paysId)
        {
            try
            {
                var res = await _parkingService.GetVillesByPays(paysId);
                //List<ReadParkVilleDTO> resmap = _mapper.Map<List<ReadParkVilleDTO>>(res);

                return Ok(res);

            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucun parking n'est present dans le Pays {paysId}!");

                //return NotFound($"Aucun parking n'est present dans le Pays {paysId}");
            }
        }


        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet("Employee/{employeeId:int}")]
        public async Task<IActionResult> GetParkingByEmployee(int employeeId)
        {
            try
            {
                var res = await _parkingService.GetParkingEmployee(employeeId);
                ReadParkEmpWorkDTO p = _mapper.Map<ReadParkEmpWorkDTO>(res);

                return Ok(p);

            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucun parking n'employe pas l'Employee {employeeId}!");

               // return NotFound($"Aucun parking n'employe pas l'Employee {employeeId}");
            }
        }

        [HttpGet("Tout-Les-Parkings")]
        public async Task<IActionResult> GetAllParkings()
        {
            try
            {
                var res = await _parkingService.GetParkingsAll();
                List<ReadAllParkingsDTO> resmap = _mapper.Map<List<ReadAllParkingsDTO>>(res);//PouR la Premiere partie parti du aceil admin

                return Ok(resmap);

            }
            catch (Exception ex)
            {
                //serilog pour logger l'erreur de l'exception

                return NotFound($"Aucun parking n'est present dans le liste ");
            }
        }

        //[Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet("id")]
        public async Task<ActionResult<ReadParkingDTO>> GetParking(int id)
        {

            try
            {
                var parking = await _parkingService.GetParkingById(id);
                ReadParkingDTO p = _mapper.Map<ReadParkingDTO>(parking);
                return p;
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le parking {id} n'a pas été trouvé !");
            }
            
        }
    }
}
