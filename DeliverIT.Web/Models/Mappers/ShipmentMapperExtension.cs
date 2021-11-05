using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models.Mappers
{
    public static class ShipmentMapperExtension
    {
        public static ShipmentDTO GetShipmentDTO(this ShipmentViewModel shipment)
        {
            return new ShipmentDTO
            {
                ArrivalDate = shipment.ArrivalDate.Value.ToShortDateString(),
                DepartureDate = shipment.DepartureDate.Value.ToShortDateString(),
                DestinationWareHouseId = shipment.DestinationWareHouseId ?? 0,
                OriginWareHouseId = shipment.OriginWareHouseId ?? 0,
                StatusId = shipment.StatusId ?? 1
            };
        }
        public static ShipmentViewModel GetShipmentViewModel(this ShipmentDTO shipment)
        {
            return new ShipmentViewModel
            {
                ArrivalDate = DateTime.Parse(shipment.ArrivalDate),
                DepartureDate = DateTime.Parse(shipment.DepartureDate),
                DestinationWareHouseId = shipment.DestinationWareHouseId,
                OriginWareHouseId = shipment.OriginWareHouseId,
                StatusId = shipment.StatusId,
                Status = shipment.Status
            };
        }
    }
}
