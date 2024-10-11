using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Controllers
{
    [Route("api/Pays")]
    [ApiController]
    public class PaysController : ControllerBase
    {

        private readonly IPaysService _paysService;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        public PaysController(IAdminService adminService, IPaysService paysService, IMapper mapper)
        {
            _adminService = adminService;
            _paysService = paysService;
            _mapper = mapper;
        }


        //[HttpGet]
        //public ActionResult<List<PaysDto>> Get()
        //{
        //    List<PaysBO> result = _adminService.GetAllPays().Result ;
        //    return Ok(result.Select(p=> new PaysDto() { Nom = p.PAYS_Nom, PremierAdresseParking= p.Adresse })); //==> Mappers
        //}
        
        [HttpPost]
        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> CreatePays(string nomPays)
        {
            try
            {
                await _adminService.AddNewPays(nomPays);
                string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                string uri = $"{baseUrl}?nomPays={nomPays}";

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadPaysDTO>>> GetPays()
        {
            try
            {
               var pays = await _paysService.GetAllPays();
               var rep = _mapper.Map<List<ReadPaysDTO>>(pays);
                return Ok(rep);
            }
            catch (Exception ex )
            {

                return NotFound($"Aucune Pays n'as était trouvé ! ");
            }
            
        }
    }
}
