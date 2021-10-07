namespace DeliverIT.Models.DatabaseModels
{
    public class Employee : AppUser
    {
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
