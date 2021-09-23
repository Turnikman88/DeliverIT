using System;
using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Shipment
    {
        public int Id { get; set; }
        public int OriginWareHouseId { get; set; }
        public virtual WareHouse OriginWareHouse { get; set; }
        public int DestinationWareHouseId { get; set; }
        public virtual WareHouse DestinationWareHouse { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public ICollection<Parcel> Parcels { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
