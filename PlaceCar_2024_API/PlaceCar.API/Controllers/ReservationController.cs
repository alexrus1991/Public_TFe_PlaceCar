using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.Exceptions.Business;
using PlaceCar.Domain.StatisticObjects;
using System.Text.Json;

namespace PlaceCar.API.Controllers
{
    [Route("api/Reservations")]
    
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ReservationController(IClientService clientService, IMapper mapper, IReservationService reservationService)
        {
            _clientService = clientService;
            _mapper = mapper;
            _reservationService = reservationService;
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpPost("Client/NewReservation")]
        public async Task<IActionResult> CreateReservation(AddResDTO resDTO)
        {
            try
            {
                if (resDTO != null)
                {
                    var reservation = _mapper.Map<AddResBO>(resDTO);
                    await _clientService.AddReservationClient(reservation);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?placeId={resDTO.PlaceId}&clientId={resDTO.ClientId}&formuleId={resDTO.FormPrixId}";

                    return Created(uri, reservation);
                    //return Ok(reservation);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"La réservation n'a pas pu être créé, veuillez recommencer ");
            }
            
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/NonCloture")]
        public async Task<ActionResult<List<ReadResClientDTO>>> GetReservationsNonClotureesClient(int clientId)
        {
            try
            {
                var res = await _clientService.GetReservationsClient(clientId);
                var resmap = _mapper.Map<List<ReadResClientBo>>(res);
                return Ok(resmap);

            }
            catch (Exception)
            {

                return NotFound($"Aucune réservation non cloturées pour le client {clientId}");
            }
        }

        [HttpGet("Parking/{parkingId:int}/NonCloture")]
        public async Task<ActionResult<List<ReadReservationParkingDTO>>> GetReservationsNonClotureesParking(int parkingId)//PouR la deuxieme parti du aceil admin
        {
            try
            {
                var res = await _reservationService.GetReservationsParking(parkingId);
                var resmap = _mapper.Map<List<ReadReservationParkingBO>>(res);
                return Ok(resmap);

            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"ucune réservation non cloturées pour le client {parkingId}");
                //return NotFound($"Aucune réservation non cloturées pour le client {parkingId}");
            }
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/Cloture/")] 
        public async Task<ActionResult<List<ReadResClientDTO>>> GetReservationsClotureesClient(int clientId)
        {
            try
            {
                var res = await _clientService.GetReservationsClient(clientId, true);
                var resmap = _mapper.Map<List<ReadResClientBo>>(res);
                return Ok(resmap);
              
            }
           
            catch (Exception ex)
            {
                
                if(ex.Message !="") return BadRequest(ex.Message);
                else return NotFound($"Aucune réservation cloturées pour le client {clientId}");
            }
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpPut("Client/{clientId:int}/Reservation/{reservationId:int}/Cloture/")]
        public async Task<IActionResult> ClotureReservationClient(int clientId, int reservationId)
        {
            try
            {
                var res = await _clientService.UpdateReservationCloturer(clientId, reservationId);
                
                if(res == null)
                {
                    //return NotFound(new { clientId, reservationId });
                    return NoContent();
                }
                else
                {
                  
                    return Ok(new { clientId, reservationId });
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"La réservation {reservationId} n'a pas pu être clôturé");

            }
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpPut("Client/{clientId:int}/")] 
        public async Task<IActionResult> UpdateReservationClient(UpdateClientResDTO updateClientRes)
        {
            try
            {
                var resNew = _mapper.Map<UpdateClientResBO>(updateClientRes);
                var res = await _reservationService.UpdateReservationClient(resNew);
                var rep =  _mapper.Map<ReadResClientDTO>(res);
                //return Ok(res);
                
                if (rep == null)
                {
                    
                    return NoContent();
                }
                else
                {

                    return Ok(new { rep.RES_Id });
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Les modifications apportées à cette réservation n'ont pas pu être effectuées");

            }
        }

       // [Helper.Authorize(Roles = "Client")]
        [HttpDelete("Client/{clientId:int}/Reservation/{reservationId:int}/Suprimer")]
        public async Task<IActionResult> DeleteReservation(int reservationId, int clientId)
        {
            try
            {
                await _reservationService.DeleteReservation(reservationId, clientId);
                //string val = JsonSerializer.Serialize(reservationId);

                //Response.Headers.Add("Deleted Ressource", val);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"La suppression de la réservation n'a pas pu être effectué");
            }
        }

        [HttpGet("Statistics-Depart-Arrive/Parking/{parkingid:int}")]

        public async Task<ActionResult<StatReservationDebFinBO>> GetTauxOccupationGlobale(int parkingid, DateTime date)
        {
            try
            {
                var repStat = await _reservationService.GetReservationsDebutEtFinStats(parkingid, date);
                return Ok(repStat);
            }
            catch (Exception)
            {
                return NotFound($"Aucune Reservation trouvée pour le parking {parkingid}");
                throw;
            }
        }

        [HttpGet("Count-Nombe-Reservations/Parking/{parkingid:int}")]

        public async Task<ActionResult<int>> GetNombreReservation(int parkingid, DateTime date)
        {
            try
            {
                int count = await _reservationService.GetNombreRes(parkingid,date);
                return Ok(count); ;
            }
            catch (Exception)
            {
                return NotFound($"Aucune Reservation trouvée pour le parking {parkingid}");
                throw;
            }
        }

        [HttpGet("Count-Nombe-Reservations-Total-Du-Mois/Parking/{parkingid:int}")]
        public async Task<ActionResult<int>> GetNombreReservationDuMois(int parkingid, DateTime date)
        {
            try
            {
                int count = await _reservationService.GetNombreResMois(parkingid, date);
                return Ok(count); ;
            }
            catch (Exception)
            {
                return NotFound($"Aucune Reservation trouvée pour le parking {parkingid}");
                throw;
            }
        }
    }
}
