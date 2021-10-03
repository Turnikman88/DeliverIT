using DeliverIT.Models.Contracts;
using System;
using System.Collections.Generic;

namespace DeliverIT.Models.DatabaseModels //ToDo: ENUM?
{
    public partial class Status : IDeletable
    {
        public Status()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
