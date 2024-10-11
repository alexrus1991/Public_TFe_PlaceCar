using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.EnumsRP;
using System.Linq;
using System.Security.Claims;

namespace PlaceCar.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string? _Roles;
        
        public string Roles { get => _Roles; set => _Roles = value; }

        public  void OnAuthorization(AuthorizationFilterContext context)
        {
            
            //if (context.HttpContext.Request.Headers["origin"] != "{URL FRONT END ANGULAR!!!}") context.Result = new UnauthorizedResult();

            ClaimsPrincipal UserConnected = context.HttpContext.User; //Remplit automatiquement lorsqu'on est connecté
            IEnumerable<Claim> claims = UserConnected.Claims;
            if(Roles!=null)
            {
                if(claims.Where(c=>c.Type== ClaimTypes.Role).Select(c=>c.Value).Count(v=> Roles.Contains(v))>0)
                {
                    if(Roles.Contains(RoleEnum.Employee.ToString()) && !Roles.Contains(RoleEnum.Client.ToString())) //gérer le basculement vers l'espace client dans Angular!!
                    {
                        //if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 8)
                        //{
                        //    //l'employé ne peut se connecter que pendant les heures de bureau
                        //    context.Result = new UnauthorizedObjectResult("L'employé ne peut pas se connecter en dehors des heures de bureau");
                        //}
                    }
                }
                else
                {
                    //Je n'ai pas le rôle nécessaire
                    context.Result = new UnauthorizedResult();
                }
            }    
        }

        //Déplacer vers un service
        //private async Task<Personne> getPermission(int RoleId)
        //{
        //    return await _unitOfWork.Persmission.GetPersonneById(RoleId);
        //}
    }

}
