using System.Collections.Generic;

namespace DeliverIT.Services.DTOs
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public int OriginWareHouseId { get; set; }
        public int DestinationWareHouseId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

        public List<string> Parcels = new List<string>();
    }
}
