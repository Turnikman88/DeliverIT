using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Customer
    {
        public Customer()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
