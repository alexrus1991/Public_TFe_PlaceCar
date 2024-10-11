using AutoMapper;
using PlaceCar.Application.Interfaces.Services;
using PlaceCar.Application.Interfaces.UnitOfWork;
using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using PlaceCar.Domain.Exceptions.Business;
using PlaceCar.Domain.StatisticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper2;

        public PlaceService(IUnitOfWork unitOfWork, IMapper mapper2)
        {
            this.unitOfWork = unitOfWork;
            this.mapper2 = mapper2;
        }

        public async Task<List<ReadPlacesLibreBO>> GetPlacesLibresByParkingId(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ArgumentNullException("Parking Id est vide !!"); }
                else
                {
                    List<PlaceParking> lesplacesLibres = await unitOfWork.Place.GetPlacesLibresInParking(parkingId, date);

                    var list = mapper2.Map<List<ReadPlacesLibreBO>>(lesplacesLibres);
                    return list;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<PlaceStatusBO>> GetOccupationPlaces(int parkingId, int etageNum, DateTime date, DateTime? dateDebut, DateTime? dateFin)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null)
                {
                    throw new ArgumentNullException("Parking Id est vide !!");
                }

                // Fetch the occupation status for the specified floor
                var placeStatuses = await unitOfWork.Place.GetOccupationPlacesForFloor(parkingId, etageNum, date,dateDebut,dateFin);
                return placeStatuses;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<StatPlacesBo> GetOccupationPlaces(int parkingId, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ArgumentNullException("Parking Id est vide !!"); }
                else
                {
                    var rep = await unitOfWork.Place.GetOccupationPlacesForDay(parkingId, date);
                    return rep;

                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<StatTauxReservationParEtageBo> GetReservationStatParEtageParking(int parkingId, int etageNumero, DateTime date)
        {
            try
            {
                var parking = await unitOfWork.Parking.GetParkingById(parkingId);
                if (parking == null) { throw new ParkingNotFoundExeption(parkingId); }
                else
                {
                    var rep = await unitOfWork.Place.GetReservationStatParEtage(parkingId, etageNumero, date);
                    return rep;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
