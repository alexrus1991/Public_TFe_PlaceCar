using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using PlaceCar.API.Models;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Services;
using System;

namespace PlaceCar.API.Controllers
{
    public class GeoapifyController : ControllerBase//permet d’interagir avec l'API Geoapify pour transformer une adresse textuelle en coordonnées géographiques
    {
        private readonly HttpClient _client;

        public GeoapifyController()
        {
            _client = new HttpClient();
        }

        [HttpGet("{address}")]
        public async Task<string> GeocodeAddress(string address)
        {
            string url = $"https://api.geoapify.com/v1/geocode/search?text={address}&apiKey=fdde1943343e4c329d609d1cada9ad29";//Construit une URL avec l'adresse fournie et l'API key de Geoapify.

            using HttpResponseMessage response = await _client.GetAsync(url);//requête HTTP vers l'API Geoapify pour obtenir les coordonnées géographiques
            string result = await response.Content.ReadAsStringAsync(); //Récupère la réponse en format JSON.


            return result;
        }
    }
}
