using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Controllers
{
    [Route("api/FormuleDePrix")]
    [ApiController]
    public class FormulePrixController : ControllerBase
    {
        private readonly IFormulePrixService _formulePrixService;
        private readonly IParkingService parkingService;
        private readonly IMapper _mapper;

        public FormulePrixController(IParkingService parkingService, IMapper mapper, IFormulePrixService formulePrixService)
        {
            this.parkingService = parkingService;
            _mapper = mapper;
            _formulePrixService = formulePrixService;
        }

        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> CreatePrixFormuleParking([FromBody] AddFormPrixDTO formPrixDTO)
        {
            try
            {
                if (formPrixDTO != null)
                {
                    var formule = _mapper.Map<AddFormuleBO>(formPrixDTO);

                    await parkingService.AddFormPrix(formule);
                    
                }
                return Ok();
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"La formule de prix n'a pas pu être créée correctement ");
            }
           
        }

        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpPut("Parking/{parkingId:int}/Formule/{formuleId:int}/")]
        public async Task<IActionResult> UpdateFormuleDePrix(int formuleId, decimal prix,int parkingId)
        {
            try
            {
                var res = await _formulePrixService.UpdateFormulePrix(formuleId, prix,parkingId);

                if (res == null)
                {
                    return NotFound(new {formuleId,parkingId});
                }
                else
                {

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le modification du prix n'a pas pu être effectuer ");
            }
        }

        [HttpGet("Parking/{parkingId:int}")]
        public async Task<ActionResult<List<ReadForulesParkingDTO>>> GetFormulesPrixParking(int parkingId)
        {
            try
            {
                var formules = await _formulePrixService.GetFormulesParkingId(parkingId);


                var lst = _mapper.Map<List<ReadForulesParkingBO>> (formules);
                return Ok(lst);
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucune formule de prix n'a pu être trouvé pour le parking désiré !");
            }
        }

        //[HttpGet("Nouvelle-FormileDePrix/Parking/{parkingId:int}/Reservation/")]
        //public async Task<ActionResult<List<ReadForulesParkingDTO>>> GetFormulesPrixParkingUpdateReservation(int parkingId,int resId)
        //{
        //    try
        //    {
        //        var formules = await _formulePrixService.GetFormulesParkingId(parkingId);

        //        var lst = _mapper.Map<List<ReadForulesParkingBO>>(formules);
        //        return Ok(lst);
        //    }
        //    catch (Exception ex)
        //    {

        //        if (ex.Message != "") return BadRequest(ex.Message);
        //        else return NotFound($"Aucune formule de prix n'a pu être trouvé pour le parking désiré !");
        //    }
        //}

        //[HttpGet("Formule-Correspendante/Parking/{parkingId:int}")]
        //public async Task<ActionResult<List<ReadForulesParkingDTO>>> GetFormulesPrixParkingCombine(DateTime dateDeb, DateTime dateFin,int parkingId)
        //{
        //    try
        //    {
        //        var formules = await _formulePrixService.GetFormulesParkingId(parkingId);
        //        var lst = _mapper.Map<List<ReadForulesParkingBO>>(formules);
        //        return Ok(lst);
        //    }
        //    catch (Exception ex)
        //    {

        //        if (ex.Message != "") return BadRequest(ex.Message);
        //        else return NotFound($"Aucune formule de prix n'a pu être trouvé pour le parking désiré !");
        //    }
        //}

        [HttpGet("Formule-Correspendante/Parking/{parkingId:int}")]
        public async Task<ActionResult<List<FormuleOptionDTO>>> CalculePrix(int parkingId, DateTime dateDeb, DateTime? dateFin)
        {
            try
            {
                if (dateFin!=null && dateDeb >= dateFin)
                {
                    return BadRequest("Date de debut doit etre plus grande ou egale à la date de fin .");
                }

                var result = await _formulePrixService.CalculePrixsAsync(parkingId, dateDeb, dateFin);
                var lst = _mapper.Map<List<ReadFormuleOptionBO>>(result.OrderBy(m=>m.Total).Take(2)); //on propose les 2 moins chers
                return Ok(lst);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucune formule de prix n'a pu être trouvé pour le parking désiré !");
            }
           
        }
    }
}   
