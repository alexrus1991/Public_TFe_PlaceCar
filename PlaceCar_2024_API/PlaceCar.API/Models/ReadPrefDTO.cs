﻿namespace PlaceCar.API.Models
{
    public class ReadPrefDTO
    {
        public int PARK_Id { get; set; }
        public string? PARK_Nom { get; set; }
        public int PLA_Etage { get; set; }
        public int PLA_NumeroPlace { get; set; }
        public int PLA_Id { get; set; }
    }
}
