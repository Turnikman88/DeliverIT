using DeliverIT.Models.Contracts;

namespace DeliverIT.Models.DatabaseModels
{
    public class Employee : AppUser, IDeletable
    {
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
