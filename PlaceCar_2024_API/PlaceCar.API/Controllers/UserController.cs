using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UserController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IResult> Login(string email, string password)
        {
            try
            {

                //var context = _httpContextAccessor.HttpContext;

                var token = await _authService.Login(email, password);
                


                return Results.Ok(new { token });//new { token }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //[Helper.Authorize()]
        [HttpPost("Logout")]
        [Helper.Authorize(Roles = "Client,Admin,Employee,SuperAdmin")]
        public async Task<IResult> Logout()
        {
            try
            {
                //await _authService.Logout();
                //return Results.Ok("Déconnecté avec succès.");

                //var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                //await _authService.Logout(token);
                //return Results.Ok("Déconnecté avec succès.");
                return Results.Ok("Token must be deleted from client storage or wait until it expired! ");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
                throw;
            }
        }

        [HttpPost("NouveauClient")]
       // [Helper.Authorize(Roles = "Client,Admin,Employee")]
        public async Task<ActionResult> CreateClient([FromBody] AddClientDto clientDto)
        {
            try
            {
                if (clientDto != null)
                {
                    if (clientDto.PERS_DateNaissance > DateTime.Now.AddYears(-18)) { throw new Exception("Vous devez avoir minimum 18 ans pour créer un compte client"); }
                    var client = _mapper.Map<PersonneBo>(clientDto);
                    await _authService.AddClient(client);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?ClientNom={clientDto.PERS_Nom}'";

                    return Created(uri, client);
                }
                else
                {
                    return BadRequest("Le client n'a pas pu être enregistrer où identifier");
                }
            }
            catch (Exception ex)
            {

                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"Le client n'a pas pu être enregistrer où identifier");
            }

        }

        [HttpPost("NouveauEmployee")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> CreateEmplyee([FromBody] AddEmplDto emplDto)
        {
            try
            {
                if (emplDto != null)
                {
                    if(emplDto.PERS_DateNaissance > DateTime.Now.AddYears(-18)) { throw new Exception("L'Employé doit avoir minimum 18 ans"); }
                    var employee = _mapper.Map<AddEmpBO>(emplDto);
                    await _authService.AddEmplyee(employee);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?parkId={emplDto.ParkingId}&clientId={emplDto.PERS_Nom}'";

                    return Created(uri, employee);
                }
                else
                {
                    return BadRequest("L'Employée n'a pas pu être enregistré ou identifié");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"L'Employée n'a pas pu être enregistré ou identifié");
            }

        }

        [HttpPost("NouveauAdmin")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] AddEmplDto emplDto)
        {
            try
            {
                if (emplDto != null)
                {
                    var employee = _mapper.Map<AddEmpBO>(emplDto);
                    await _authService.AddEmplyee(employee, true);

                    string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path}";
                    string uri = $"{baseUrl}?parkId={emplDto.ParkingId}&clientId={emplDto.PERS_Nom}'";

                    return Created(uri, employee);
                }
                else { return BadRequest("L'Administrateur n'a pas pu être enregistrer où identifier"); }
            }
            catch (Exception ex) 
            {
                if (ex.Message != "") return BadRequest(ex.Message);
                else return NotFound($"L'Administrateur n'a pas pu être enregistrer où identifier");
            }
        }
    }
}
