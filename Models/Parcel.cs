#nullable disable

namespace DeliverIT.Models
{
    public partial class Parcel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int WareHouseId { get; set; }
        public double Weight { get; set; }
        public int CategoryId { get; set; }
        public bool DeliverToAddress { get; set; }
        public int ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
