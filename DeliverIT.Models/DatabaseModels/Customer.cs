using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Customer
    {
        public Customer()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
