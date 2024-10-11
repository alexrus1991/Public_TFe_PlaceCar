using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Domain.BusinessObjects;
using System.Security.Claims;
using PlaceCar.API.Helper;
using System.Security.Permissions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaceCar.API.Controllers
{
    [Route("api/Clients")]
    [ApiController]
    public class ClientColtroller : ControllerBase
    {
        
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientColtroller(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
            
        }

        [Helper.Authorize(Roles = "Client,Admin")]
        [HttpGet("id")]
        public async Task<ActionResult<ReadClientDto>> GetClient(int id)
        {
            
            var client = await _clientService.GetCliById(id);
            ReadClientDto cliDto = _mapper.Map<ReadClientDto>(client);
            return cliDto;
        }

        

        [Helper.Authorize(Roles = "Employee,Admin")]
        [HttpPost("Employee/{employeeId:int}/")]
        public async Task<ActionResult> CreateEmployeClient(int employeeId)
        {
            try
            {
                if (employeeId != null)
                {

                    await _clientService.AddEmplyeClient(employeeId);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?employeeId={employeeId}'";

                    return Created(uri, employeeId);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        [Helper.Authorize(Roles = "Client")]
        [HttpPut("Client/{clientId:int}")]
        public async Task<ActionResult<bool>> UpdateClientInfo( [FromBody] UpdatePresonCLiDto presonCLiDto)
        {
            try
            {
                var UpdtInfo = _mapper.Map<UpClientBO>(presonCLiDto);
                await _clientService.UpdateCliInfo(UpdtInfo);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        [Helper.Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetClientNombre()
        {
            int count = await _clientService.GetNombreClients();
            return Ok(count);
        }

       
    }
}
