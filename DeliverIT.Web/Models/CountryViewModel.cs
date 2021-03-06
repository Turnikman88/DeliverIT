using DeliverIT.Services.DTOs;
using DeliverIT.Web.Models.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Web.Models
{
    public class CountryViewModel : ISearchable
    {
        public int Id { get; set; }
            
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value for {0} must be between {2} and {1}.")]
        public string Name { get; set; }

        public IEnumerable<CountryDTO> Countries { get; set; }

        public string FilterTag { get; set; }
    }
}
