using DeliverIT.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Models.DatabaseModels
{
    public abstract class AppUser : IDeletable
    {
        public int Id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
