using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Interfaces.Repositories
{
    public interface IPlacesRepository
    {
        Task AddPlaceParking(PlaceParking placeParking);
        Task<List<PlaceParking>> GetPlacesLibresInParking(int parkingId, DateTime date);
        //Task<bool> PlacePasReservée(int placeId, DateTime dateDebut, DateTime dateFin);
        Task<List<PlaceStatusBO>> GetOccupationPlacesForFloor(int parkingId, int etageNum, DateTime date,DateTime? dateDebut, DateTime? dateFin);
        Task<PlaceParking> GetPlaceById(int  placeId);
        Task<StatPlacesBo> GetOccupationPlacesForDay(int parkingId, DateTime date);
        Task<StatTauxReservationParEtageBo> GetReservationStatParEtage(int parkingId, int etageId, DateTime date);
        Task UpdatePlaceParking(PlaceParking place, Reservation reservation);
        Task UpdatePlaceParkinglibre(PlaceParking place, Reservation reservation);
    }
}
