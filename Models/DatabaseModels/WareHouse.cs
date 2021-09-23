using System.Collections.Generic;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class WareHouse
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

        public virtual ICollection<Parcel> Parcels { get; set; }
        public virtual ICollection<Shipment> ShipmentDestinationWareHouses { get; set; }
        public virtual ICollection<Shipment> ShipmentOriginWareHouses { get; set; }
    }
}
