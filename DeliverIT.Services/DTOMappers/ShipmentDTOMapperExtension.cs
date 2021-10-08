using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIT.Services.DTOMappers
{
    public static class ShipmentDTOMapperExtension
    {
        public static ShipmentDTO GetDTO(this Shipment shipment)
        {
            if (shipment is null || shipment.ArrivalDate == null || shipment.DepartureDate == null
                || shipment.OriginWareHouseId <= 0 || shipment.DestinationWareHouseId <= 0
                || shipment.StatusId <= 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            return new ShipmentDTO
            {
                Id = shipment.Id,
                ArrivalDate = shipment.ArrivalDate,
                DepartureDate = shipment.DepartureDate,
                OriginWareHouseId = shipment.OriginWareHouseId,
                DestinationWareHouseId = shipment.DestinationWareHouseId,
                StatusId = shipment.StatusId,
                Status = shipment.Status.Name,
                Parcels = shipment.Parcels.Select(x => $"Id: {x.Id}").ToList()
            };
        }        

        public static Shipment GetEntity(this ShipmentDTO shipment)
        {
            if (shipment is null || shipment.ArrivalDate == null || shipment.DepartureDate == null
                || shipment.OriginWareHouseId <= 0 || shipment.DestinationWareHouseId <= 0
                || shipment.StatusId <= 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            return new Shipment
            {
                Id = shipment.Id,
                ArrivalDate = shipment.ArrivalDate,
                DepartureDate = shipment.DepartureDate,
                OriginWareHouseId = shipment.OriginWareHouseId,
                DestinationWareHouseId = shipment.DestinationWareHouseId,
                StatusId = shipment.StatusId
            };
        }
    }
}
