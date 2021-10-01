using DeliverIT.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Employee : IDeletable
    {
        public int Id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
