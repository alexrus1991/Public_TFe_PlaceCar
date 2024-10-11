using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlaceCar.Infrastructure;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace PlaceCar.API.Extentions
{
    public static class ApiExtentions
    {
 
        public static void AddApiExtentions(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();//ire le fichier de configuration appsettings.json.,et convertie en un objet JwtOptions,

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//indique que l'authentification JWT sera utilisée
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>//définit des paramètres de validation
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtOptions.ValidIssuer,
                        ValidAudience = jwtOptions.ValidAudience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    
                });

            //permettant de restreindre l'accès à certaines paeries en fonction de l'authentification du token
            services.AddAuthorization();
        }
    }
    //options.Events = new JwtBearerEvents()
    //{
    //    OnMessageReceived = context =>
    //    {
    //        context.Token = context.Request.Cookies["test-cooki"];

    //        return Task.CompletedTask;
    //    }
    //};
    //services.AddAuthorization(options =>
    //{
    //    options.AddPolicy("AdminPolicy", policy =>
    //    {
    //        policy.RequireClaim("Admin", "true");
    //    });
    //});
}
