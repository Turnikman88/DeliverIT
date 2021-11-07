namespace DeliverIT.Services.DTOs
{
    public class ParcelDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ShipmentId { get; set; }
        public int? WareHouseId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Weight { get; set; }
        public bool DeliverToAddress { get; set; }
        public string ShipmentStatus { get; set; }
    }
}
