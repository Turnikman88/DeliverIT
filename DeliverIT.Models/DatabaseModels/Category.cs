using DeliverIT.Models.Contracts;
using System;
using System.Collections.Generic;

#nullable disable

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Category : IDeletable
    {
        public Category()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
