using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/TypesDeFormules")]
    [ApiController]
    public class FormuleTypeController : ControllerBase
    {
        private readonly IFormuleTypeService formuleTypeService;
        private readonly IMapper _mapper;

        public FormuleTypeController(IFormuleTypeService formuleTypeService, IMapper mapper)
        {
            this.formuleTypeService = formuleTypeService;
            _mapper = mapper;
        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> CreateTypeFormule([FromBody] AddFormuleTypeDTO formuleTypeDTO)
        {
            if(formuleTypeDTO != null) 
            {
                try
                {
                    var formuletype = _mapper.Map<AddTypeFormBO>(formuleTypeDTO);
                    await formuleTypeService.AddTypeFormule(formuletype);

                    return Ok();
                }
                catch (Exception ex)
                {
#if DEBUG
                    return BadRequest(ex);
#else
                    return BadRequest("Impossible d'enregistrer le formule");
#endif

                }
            }
            else { return BadRequest(); }
        }

        [Helper.Authorize(Roles = "Employee,Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<List<ReadTypeFormDTO>>> GetAllTypes()
        {
            try
            {
                var items = await formuleTypeService.GetTypeForm();
                return Ok(_mapper.Map<List<ReadTypeBO>>(items));
            }
            catch (Exception ex) { return BadRequest(); }
        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("Update-Description/{typeId:int}")]
        public async Task<ActionResult<bool>> UpdateDescriptionFormuleType(int typeId,string description)
        {
            try
            {
                await formuleTypeService.UpdateTypeDescription(typeId, description);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("Update-Titre/{typeId:int}")]
        public async Task<ActionResult<bool>> UpdateTitreFormuleType(int typeId, string titre)
        {
            try
            {
                await formuleTypeService.UpdateTypeTitre(typeId, titre);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //[HttpGet("Formule-Correspendante")]
        //public async Task<ActionResult<List<ReadTypeFormDTO>>> GetType(DateTime dateDeb,DateTime dateFin)
        //{
        //    try
        //    {
        //        var items = await formuleTypeService.GetCombTypeForm(dateDeb,dateFin);
        //        return Ok(_mapper.Map<List<ReadTypeBO>>(items));
        //    }
        //    catch (Exception ex) { return BadRequest(); }
        //}

       
    }
}
