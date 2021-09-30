using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Services.DTOs
{
    public class WareHouseDTO
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<string> Parcels = new List<string>();

        public List<string> ShipmentDestinationWareHouses = new List<string>();

        public List<string> ShipmentOriginWareHouses = new List<string>();
    }
}
