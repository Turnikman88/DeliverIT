using System.Collections.Generic;


namespace DeliverIT.Models.DatabaseModels
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
