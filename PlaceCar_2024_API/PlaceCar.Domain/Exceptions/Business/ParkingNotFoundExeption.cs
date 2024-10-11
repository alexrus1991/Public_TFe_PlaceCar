using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Exceptions.Business
{
    public class ParkingNotFoundExeption : Exception
    {
        public ParkingNotFoundExeption(int parkingId) : base($"Parking avec identifient {parkingId} ne se trouve pas dans le Group PlaceCar")
        {
        }
    }
}
