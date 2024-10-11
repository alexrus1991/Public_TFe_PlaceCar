using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.StatisticObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/Factures")]
    [ApiController]
    public class FactureController : ControllerBase
    {
        private readonly IFactureService _factureService;
        private readonly IMapper _mapper;

        public FactureController(IFactureService factureService, IMapper mapper)
        {
            _factureService = factureService;
            _mapper = mapper;
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/NonPaye")]
        public async Task<ActionResult<List<ReadFactureDTO>>> GetFacturesNonPaye(int clientId)
        {
            try
            {
                var factures = await _factureService.GetFacturesNonPaye(clientId);
                var lstFactures = _mapper.Map<List<ReadFactureDTO>>(factures);
                return Ok(lstFactures);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Vous ne possédez aucune facture impayée");
                //return NotFound($"Aucune réservation non cloturées pour le client {clientId}");
            }
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/Paye")]
        public async Task<ActionResult<List<ReadFactureDTO>>> GetFacturesPaye(int clientId)
        {
            try
            {
                var factures = await _factureService.GetFacturesPaye(clientId);
                var lstFactures = _mapper.Map<List<ReadFactureDTO>>(factures);
                return Ok(lstFactures);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
               else return Ok($"Aucune facture non cloturées pour le client {clientId}"); //NotFound($"Aucune réservation non cloturées pour le client {clientId}");
            }
        }

        [HttpGet("Statistics/parking/{parkingId:int}")]
        public async Task<ActionResult<StatFacturesBO>> GetFacturesStatParJour(int parkingId,DateTime date)
        {
            try
            {
                var stats = await _factureService.GetStatFacturesParJour(parkingId,date);
                
                return Ok(stats);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
               else return NotFound($"Aucune Facture non payée pour le parking {parkingId}");
            }
        }
    }
}
