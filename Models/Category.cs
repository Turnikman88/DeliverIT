using System.Collections.Generic;

#nullable disable

namespace DeliverIT.Models
{
    public partial class Category
    {
        public Category()
        {
            Parcels = new HashSet<Parcel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
