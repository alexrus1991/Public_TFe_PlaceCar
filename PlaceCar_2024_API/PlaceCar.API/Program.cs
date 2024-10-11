using PlaceCar.Infrastructure;
//using PlaceCar.Application;
using Microsoft.EntityFrameworkCore;
using PlaceCar.Application.Interfaces.Repositories;
using PlaceCar.Infrastructure.PlaceCar_Repositories;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using PlaceCar.Application.Interfaces.Provider;
using PlaceCar.Application.Interfaces.PwdHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PlaceCar.API.Extentions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.OpenApi.Models;
using PlaceCar.Domain.Stripe;
using Stripe;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
//builder.Services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
//var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

builder.Services.AddApiExtentions(configuration); //Un middleware qui configure le principe du jwt bearer


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpContextAccessor();

//Stripe payement

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeOptions"));
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeOptions:PrivateKey");

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PlaceCar API", Version = "v1" });// documentation Swagger pour la version
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme// indique que l'API utilise le schéma d'authentification Bearer (JWT)
    {
        In = ParameterLocation.Header,// jeton JWT doit être inclus dans l'en-tête de la requête HTTP
        Description = "Encoder votre toke; Exemple : Bearer {token}",//description de la manière dont le jeton JWT doit être inclus dans l'en-tête
        Name = "Authorization",//défini sur Authorization, ce qui est une convention courante
        Type = SecuritySchemeType.Http,//schéma d'authentification est basé sur HTTP
        BearerFormat = "JWT",// format attendu pour le jeton JWT
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement//spécifie les exigences de sécurité
    {                                                           //Bearer défini précédemment est requis pour toutes les opérations de l'API
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference//fait référence à une autre définition Swagger.
                {
                    Type=ReferenceType.SecurityScheme,//définition de schéma de sécurité
                    Id="Bearer"//identifiant de la référence
                }
            },
            new string[]{}//aucune autorisation supplémentaire n'est requise au-delà de celle spécifiée
        }
    });
});
builder.Services.AddDbContext<PlaceCarDbContext>(
   options =>
   {
       //options.UseSqlServer(configuration.GetConnectionString(nameof(PlaceCarDataBase)));
       options.UseSqlServer(configuration.GetConnectionString("PlaceCarDataBase"), sqloptions => { });
   });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#if DEBUG
string CorsPolicyName = "DevPolicy";
//Politique de cross-origin (CORS)
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: CorsPolicyName, policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
    }//Cette politique spécifie les règles CORS à appliquer
    );
#else
string CorsPolicyName = "ProdPolicy";
//Politique de cross-origin (CORS)
builder.Services.AddCors(
    options => 
    {
        options.AddPolicy(name: CorsPolicyName, policy => { policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyOrigin(); });
    }
    );
#endif

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped<IPaysRepository,PaysRepository>();
builder.Services.AddScoped<IPaysService,PaysService>();
    builder.Services.AddScoped<IParkingService, ParkingService>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IFormuleTypeService, FormuleTypeService>();
    builder.Services.AddScoped<IAdminService,AdminService>();
    builder.Services.AddScoped<IPlaceService, PlaceService>();
    builder.Services.AddScoped<IPreferenceService,PreferenceService>();
    builder.Services.AddScoped<IReservationService,ReservationService>();
    builder.Services.AddScoped<IFactureService,FactureService>();
    builder.Services.AddScoped<ITransactionService,TrensactionService>();
    builder.Services.AddScoped<IFormulePrixService, FormulePrixService>();
    builder.Services.AddScoped<ICompteService, CompteService>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

//Lorsque tu dois injecter des services qui attendent un repository comme paramètre du constructeur, tu dois faire l'injection du repo aussi
// le soucis qui reste dépend d'une configuration EfCore







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();
 
app.UseCors(CorsPolicyName);



//app.UseAuthorization();

app.MapControllers();
//app.MapGet("get", () =>
//{
//    return Results.Ok("ok");
//}).RequireAuthorization("AdminPolicy");
app.Run();
