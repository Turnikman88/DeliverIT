using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Category
    {
        public Category()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }

        [MinLength(2), MaxLength(20)]
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
