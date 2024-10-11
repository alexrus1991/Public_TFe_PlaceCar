using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/PlacesParking")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        //private readonly IParkingService _parkingService;
        private readonly IMapper _mapper;

        public PlaceController(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        [HttpGet("Lidre/Parking/{parkingid:int}/Date/{date:datetime}")]

        public async Task<ActionResult<List<ReadPlaceLibDTO>>> GetPlacesLibres(int parkingid, DateTime date)
        {
            try
            {             
                 var lisaplaces = await _placeService.GetPlacesLibresByParkingId(parkingid, date);

                 return Ok(_mapper.Map<List<ReadPlaceLibDTO>>(lisaplaces));             
            }
            catch (Exception)
            {
                return NotFound($"Aucune place libre cloturées pour le parking {parkingid}");
                throw;
            }
        }
       

        [HttpGet("Parking/{parkingid:int}/Etage/{etageId:int}")]
        public async Task<ActionResult<List<PlaceStatusDto>>> GetTauxOccupationGlobaleParEtage(int etageId ,int parkingid, DateTime date, DateTime? dateDebut, DateTime? dateFin)
        {
            try
            {
                //Si aucune date transmise on prend la date du jour
                //if (date == DateTime.MinValue) date = new DateTime(2024, 09, 21);

                var placeStatuses = await _placeService.GetOccupationPlaces(parkingid,etageId, date,dateDebut,dateFin);
                return Ok(_mapper.Map<List<PlaceStatusDto>>(placeStatuses.Take(18)));
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Aucune place trouvée pour le parking {parkingid} à l'étage {etageId}");
            }
        }

        [HttpGet("Occupation/Parking/{parkingid:int}")]
        public async Task<ActionResult<StatPlacesBo>> GetTauxOccupationGlobale(int parkingid, DateTime date)
        {
            try
            {
                var lisaplaces = await _placeService.GetOccupationPlaces(parkingid, date);
                return Ok(lisaplaces);
            }
            catch (Exception)
            {
                return NotFound($"Aucune place trouvée pour le parking {parkingid}");
                throw;
            }
        }

        [HttpGet("Occupation/Parking/{parkingid:int}/Etage/{etageNumero:int}")]

        public async Task<ActionResult<StatPlacesBo>> GetOccupationParEtage(int parkingid,int etageNumero, DateTime date)
        {
            try
            {
                var lisaplaces = await _placeService.GetReservationStatParEtageParking(parkingid, etageNumero, date);
                return Ok(lisaplaces);
            }
            catch (Exception)
            {
                return NotFound($"Aucune place trouvée pour le parking {parkingid}");
                throw;
            }
        }
    }
}

