using DeliverIT.Models.Contracts;
using System.Collections.Generic;

#nullable disable

namespace DeliverIT.Models.DatabaseModels
{
    public class Customer : AppUser, IDeletable
    {
        public Customer()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
