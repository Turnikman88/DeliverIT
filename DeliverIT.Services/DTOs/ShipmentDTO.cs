using DeliverIT.Models.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DeliverIT.Services.DTOs
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int OriginWareHouseId { get; set; }
        public int DestinationWareHouseId { get; set; }
        public int StatusId { get; set; }
        public List<string> Parcels = new List<string>();
    }
}
