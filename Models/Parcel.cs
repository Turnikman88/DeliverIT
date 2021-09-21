﻿#nullable disable

namespace Models
{
    public partial class Parcel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int WareHouseId { get; set; }
        public double Weight { get; set; }
        public int CategoryId { get; set; }
        public bool DeliverToAddress { get; set; }

        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
