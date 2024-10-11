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
    [Route("api/Transactions")]
    [ApiController]
    public class TrensactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TrensactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        //[Helper.Authorize(Roles = "Client")]
        //[HttpPost]
        //public async Task<ActionResult<ReadTransacDTO>> CreateTransaction([FromBody] AddTransacDTO transacDTO)
        //{
        //    try
        //    {
        //        if(transacDTO != null)
        //        {
        //            var t = _mapper.Map<AddTransacBO>(transacDTO);
        //            var rep = await _transactionService.AddTrensaction(t);
        //            ReadTransacDTO trs = _mapper.Map<ReadTransacDTO>(rep);

        //            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
        //            string uri = $"{baseUrl}?factureId={transacDTO.FactureId}&compteId={transacDTO.CompteUnId}'";

        //            return Created(uri, rep);
        //        }
        //        else { return BadRequest(); }

        //    }
        //    catch (Exception ex)
        //    {

        //        if (ex.Message != "") return BadRequest(ex.Message);
        //        else return NotFound($"La transaction n'a pas pu être effectuée ! Veuillez recommencer");
        //    }
        //}

        [Helper.Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] AddTransacDTO transacDTO)
        {
            try
            {
                if (transacDTO != null)
                {
                    var t = _mapper.Map<AddTransacBO>(transacDTO);
                    await _transactionService.AddTrensaction(t);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?factureId={transacDTO.FactureId}&compteId={transacDTO.CompteUnId}'";

                    return Created(uri, transacDTO);
                }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"La transaction n'a pas pu être effectuée ! Veuillez recommencer");
            }
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/")]
        public async Task<ActionResult<List<ReadTransacDTO>>> GettransactionsByClientId(int clientId)
        {
            try
            {
                var trensactions = await _transactionService.GetTrensactionsClient(clientId);
                var lstTrs = _mapper.Map<List<ReadTransacDTO>>(trensactions);
                return Ok(lstTrs);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucune transaction trouvé pour le client {clientId}");
               // return NotFound($"Aucune transaction trouvé pour le client {clientId}");
            }
        }

        [HttpPut("UpdateTransactionFailure")]
        public async Task<ActionResult<bool>> UpdateTransactionFailure(int factureId,string commentaire)
        {
            try
            {
                await _transactionService.UpdateTrensaction(factureId, commentaire);
                
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("PlaceCar")]
        public async Task<ActionResult<List<ReadDeataiTransacDTO>>> GetTransactionsPlaceCar(DateTime date)
        {
            try
            {
                var trensactions = await _transactionService.GetAllTrensactions(date);
                var lstTrs = _mapper.Map<List<ReadDeataiTransacDTO>>(trensactions);
                return Ok(lstTrs);
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucune transaction trouvé");
               // return NotFound($"Aucune transaction trouvé");
            }
        }
    }
}
