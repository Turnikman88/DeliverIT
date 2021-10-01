using DeliverIT.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Parcel : IDeletable
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }
        public int? WareHouseId { get; set; } // ToDo: why force me to make it nullable
        public virtual WareHouse WareHouse { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Range(0, 10000)]
        public double Weight { get; set; }
        public bool DeliverToAddress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
