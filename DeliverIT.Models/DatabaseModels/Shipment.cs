using DeliverIT.Models.Contracts;
using System;
using System.Collections.Generic;


namespace DeliverIT.Models.DatabaseModels
{
    public partial class Shipment : IDeletable
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
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
