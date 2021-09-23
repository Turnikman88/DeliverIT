#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Employee
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
    }
}
