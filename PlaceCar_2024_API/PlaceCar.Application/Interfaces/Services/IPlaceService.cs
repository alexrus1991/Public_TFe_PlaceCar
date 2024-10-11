using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<List<ReadPlacesLibreBO>> GetPlacesLibresByParkingId(int parkingId, DateTime date);
        Task<List<PlaceStatusBO>> GetOccupationPlaces(int parkingId, int etageNum, DateTime date,DateTime? dateDebut, DateTime? dateFin);
        Task<StatPlacesBo> GetOccupationPlaces(int parkingId, DateTime date);
        Task<StatTauxReservationParEtageBo> GetReservationStatParEtageParking(int parkingId, int etageId, DateTime date);


    }
}
