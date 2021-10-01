using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
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
            return new ShipmentDTO
            {
                Id = shipment.Id,
                ArrivalDate = shipment.ArrivalDate,
                DepartureDate = shipment.DepartureDate,
                OriginWareHouseId = shipment.OriginWareHouseId,
                DestinationWareHouseId = shipment.DestinationWareHouseId,
                StatusId = shipment.StatusId,
                Status = shipment.Status.Name,
                Parcels = shipment.Parcels.Select(x => $"{x.Id}").ToList()
            };
        }

        public static Shipment GetEntity(this ShipmentDTO shipment)
        {
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
