using System;
using System.Collections.Generic;

#nullable disable

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Shipment
    {
        public Shipment()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int OriginWareHouseId { get; set; }
        public virtual WareHouse OriginWareHouse { get; set; }
        public int DestinationWareHouseId { get; set; }
        public virtual WareHouse DestinationWareHouse { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public ICollection<Parcel> Parcels { get; set; }
    }
}
