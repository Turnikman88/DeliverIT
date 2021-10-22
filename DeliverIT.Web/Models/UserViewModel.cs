using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models
{
    public class UserViewModel
    {
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")] //ToDo: add error messages
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Password lenght must be {0} characters or longer")]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage = "Address name cannot be that short")]
        public string Address { get; set; }

        [MinLength(5, ErrorMessage = "City name cannot be that short")]
        public string City { get; set; }

        [MinLength(5, ErrorMessage = "Country name cannot be that short")]
        public string Country { get; set; }

        public int AddressId { get; set; }

    }
}
