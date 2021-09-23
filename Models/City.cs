using System.Collections.Generic;

#nullable disable

namespace DeliverIT.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
