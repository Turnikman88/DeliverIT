using DeliverIT.Models.Contracts;
using System;
using System.Collections.Generic;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class WareHouse : IDeletable
    {
        public WareHouse()
        {
            Parcels = new HashSet<Parcel>();
            ShipmentDestinationWareHouses = new HashSet<Shipment>();
            ShipmentOriginWareHouses = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
        public virtual ICollection<Shipment> ShipmentDestinationWareHouses { get; set; }
        public virtual ICollection<Shipment> ShipmentOriginWareHouses { get; set; }
    }
}
