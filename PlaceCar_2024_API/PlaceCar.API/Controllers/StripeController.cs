using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Domain.Stripe;
using Stripe.Checkout;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.API.Models;
using PlaceCar.Application.Services;
using AutoMapper;
using Azure;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.BusinessObjects;

namespace PlaceCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IFactureService _factureService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private int FactureId;
        public StripeController(IOptions<StripeSettings> stripeSettings, IFactureService factureService, ITransactionService transactionService, IMapper mapper)
        {
            _stripeSettings = stripeSettings.Value;
            _factureService = factureService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost("create-checkout-session")]
        [Authorize()]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)//AddTransacDTO transacDTO
        {
            if (req.CustomerId != User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value)
            {
                return BadRequest("Wrong Customer ID");
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = req.SuccessUrl,
                CancelUrl = req.FailureUrl,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Currency = "eur",
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                          Quantity=1,
                          PriceData= new SessionLineItemPriceDataOptions
                          {
                               Currency="eur",
                                ProductData = new SessionLineItemPriceDataProductDataOptions() { Name=$"Client : {req.CustomerId} - Facture {req.FactureId}"},
                                UnitAmount= req.PriceId
                          }

                    },
                },
            };

            var service = new SessionService();
            service.Create(options);
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                    PublicKey = _stripeSettings.PublicKey
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }

        //Pour le développement utiliser ngrok pour exposer le webhok pour stripe https://dashboard.ngrok.com/  !!! A autoriser avec windows defender ou autre anti-virus
        [HttpPost("ReceivePayement")]
        public async Task<IActionResult> ReceivePayement()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent =  EventUtility.ConstructEvent(json,Request.Headers["Stripe-Signature"], _stripeSettings.WHSecret);
                
                //On récupère l'id de la facture du json et on valide celle-ci dans la db
                    var options = new Stripe.Checkout.SessionGetOptions
                    {
                        Expand = new List<string> { "line_items" },
                    };
                    ////on récupère la session dans la réponse
                    Stripe.Checkout.Session obj = (stripeEvent.Data.Object as Stripe.Checkout.Session) ?? throw new Exception("Stripe session not found");
                    var service = new Stripe.Checkout.SessionService();
                    Stripe.Checkout.Session infos = await service.GetAsync(obj?.Id, options);

                    string Description = (infos.LineItems.First() as LineItem).Description;
                    if (Description == null || !Description.Contains("- Facture")) throw new Exception("Facture not found");
                    int factureId = int.Parse(Description.Split(" - Facture")[1]); 
                    FactureId = factureId;
                    //Permet de garder le numéro de la session de paiement strip afin de valider le paiement ou le refuser suivant le retour du côté angular
                    string StripeSTR = Request.Headers["Stripe-Signature"];
                 
                if (stripeEvent.Type  == Events.CheckoutSessionCompleted)  
                {
                    
                    bool status = true;
                    await _factureService.UpdateFactureStripe(factureId, StripeSTR, status);
                    /*valider la transactio ==> Plus besoinn*/
                }
                else
                {
                    //Changer commentaire transaction pour donner l'info comme quoi la facture n'a pas été payée correctement
                    string commentaire = "Echec de la transaction pour la facture" + factureId;
                    bool status = false;
                    await _factureService.UpdateFactureStripe(factureId, StripeSTR, status);
                    await _transactionService.UpdateTrensaction(factureId, commentaire);
                    //update transaction avec commentaire ECHEC
                   /*Transaction annulée*/
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
       
        
 

        //[HttpGet("VerficateFactureInfo")]
        //public async Task<ActionResult<ReadFactureStripeDto>> GetStripeFacture(int id)
        //{
        //    try
        //    {
        //        var facture = await _factureService.GetFactureStripe(id);
        //        ReadFactureStripeDto reponce = _mapper.Map<ReadFactureStripeDto>(facture);
        //        return Ok(reponce);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message != "") return BadRequest(ex.Message);
        //        else return NotFound($"La Facture demandée est introuvable"); throw;
        //    }

        //}

        //[HttpPut("ValidationPaiment")]
        //public async Task<IActionResult> UpdateStatusFacture(int id, bool status)
        //{
        //    try
        //    {

        //        await _factureService.UpdateFactureStripe(FactureId, " ", status);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        if (ex.Message != "") return BadRequest(ex.Message);
        //        else return NotFound($"Le Paiement as bien été validé"); throw;
        //    }
        //}

    }
}
