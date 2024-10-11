using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace PlaceCar.API.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/Preferences")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;
        private readonly IMapper _mapper;

        public PreferenceController(IPreferenceService preferenceService, IMapper mapper)
        {
            _preferenceService = preferenceService;
            _mapper = mapper;
        }

        //[HttpPost("Place/Parking")]

        //public async Task<IActionResult> AddPreferenceClient([FromBody] AddPrefDTO prefDTO)
        //{
        //    try
        //    {
        //        if(prefDTO != null) 
        //        {
        //            var pref = _mapper.Map<AddPrefBO>(prefDTO);
        //            await _preferenceService.AddPreferance(pref);

        //            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}"; 
        //            string uri = $"{baseUrl}?parkId={prefDTO.ParkingId}&clientId={prefDTO.ClientId}'";

        //            return Created(uri, pref);
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

        [Helper.Authorize(Roles = "Client")]
        [HttpGet("Client/{clientId:int}/Parking/{parkId:int}/")] 
        public async Task<IActionResult> GetPreferencesClient(int parkId,int clientId)
        {
            try
            {            
                var lstPref = await _preferenceService.GetPreferances(parkId, clientId);
                List< ReadPrefDTO> lst = _mapper.Map<List<ReadPrefDTO>>(lstPref);
                /*if(lst.Count == 0)
                {
                    return NotFound();
                }*/
                return Ok(lst);
                     
                           
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }

        [Helper.Authorize(Roles = "Client")]
        [HttpDelete("Place/{placeId:int}")]
        public async Task<IActionResult> DeletePreference([FromBody] AddPrefDTO prefDTO)
        {
            try
            {          
                var pref = _mapper.Map<AddPrefBO>(prefDTO);
                var p = await _preferenceService.DeletePreference(pref);
                if(p == null)
                {
                    return NotFound(_mapper.Map<AddPrefDTO>(p));
                }
                else
                {
                    string val = JsonSerializer.Serialize(prefDTO);
                  
                   // Response.Headers.Add("Deleted Ressource", val);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                //serilog pour l'exception
                return NotFound(ex.Message);
            }
        }
    }
}
