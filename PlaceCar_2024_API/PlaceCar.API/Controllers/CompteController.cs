using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/Comptes")]
    [ApiController]
    public class CompteController : ControllerBase
    {
        private readonly ICompteService _compteService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public CompteController(IClientService clientService, IMapper mapper, ICompteService compteService)
        {
            _clientService = clientService;
            _mapper = mapper;
            _compteService = compteService;
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpPost("Client/{clientId:int}")]
        public async Task<IActionResult> CreateCompteClient(AddCompteDTO compteDto)
        {
            try
            {
                if (compteDto != null)
                {
                    var compte = _mapper.Map<AddCompteBo>(compteDto);
                    await _clientService.AddCompteClient(compte);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?clientId={compteDto.ClientId}'";

                    return Created(uri, compte);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le Compte Bancaire n'a pas pu être créer");
            }
           
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/Information")]
        public async Task<IActionResult> GetCompteClient(int clientId)
        {
            try
            {
                var compte = await _compteService.GetCompteByClientId(clientId);
                var compteRep = _mapper.Map<ReadCompteCliDTO>(compte);
                return Ok(compteRep);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucun compte associé au client {clientId} n'a été trouvé !");
            }
        }
    }
}
