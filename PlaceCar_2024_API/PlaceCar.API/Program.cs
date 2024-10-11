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
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme// indique que l'API utilise le sch�ma d'authentification Bearer (JWT)
    {
        In = ParameterLocation.Header,// jeton JWT doit �tre inclus dans l'en-t�te de la requ�te HTTP
        Description = "Encoder votre toke; Exemple : Bearer {token}",//description de la mani�re dont le jeton JWT doit �tre inclus dans l'en-t�te
        Name = "Authorization",//d�fini sur Authorization, ce qui est une convention courante
        Type = SecuritySchemeType.Http,//sch�ma d'authentification est bas� sur HTTP
        BearerFormat = "JWT",// format attendu pour le jeton JWT
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement//sp�cifie les exigences de s�curit�
    {                                                           //Bearer d�fini pr�c�demment est requis pour toutes les op�rations de l'API
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference//fait r�f�rence � une autre d�finition Swagger.
                {
                    Type=ReferenceType.SecurityScheme,//d�finition de sch�ma de s�curit�
                    Id="Bearer"//identifiant de la r�f�rence
                }
            },
            new string[]{}//aucune autorisation suppl�mentaire n'est requise au-del� de celle sp�cifi�e
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
    }//Cette politique sp�cifie les r�gles CORS � appliquer
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

//Lorsque tu dois injecter des services qui attendent un repository comme param�tre du constructeur, tu dois faire l'injection du repo aussi
// le soucis qui reste d�pend d'une configuration EfCore







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
