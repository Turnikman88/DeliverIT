﻿using System.Collections.Generic;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
