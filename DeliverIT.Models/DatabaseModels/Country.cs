using DeliverIT.Models.Contracts;
using System;
using System.Collections.Generic;


namespace DeliverIT.Models.DatabaseModels
{
    public partial class Country : IDeletable
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }


        public virtual ICollection<City> Cities { get; set; }
    }
}
