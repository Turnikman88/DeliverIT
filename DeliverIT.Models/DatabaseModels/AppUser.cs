using DeliverIT.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Models.DatabaseModels
{
    public abstract class AppUser : IdentityUser<int>, IDeletable
    {
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
