using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Web.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password lenght must be {1} characters or longer")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [MinLength(8, ErrorMessage = "Password lenght must be {1} characters or longer")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Address name cannot be that short")]
        public string Address { get; set; }

        [Required]        
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public int? AddressId { get; set; }

    }
}
