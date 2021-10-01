using System.Collections.Generic;

namespace DeliverIT.Models.DatabaseModels
{
    public partial class Address
    {
        public Address()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            WareHouses = new HashSet<WareHouse>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string StreetName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<WareHouse> WareHouses { get; set; }
    }
}
