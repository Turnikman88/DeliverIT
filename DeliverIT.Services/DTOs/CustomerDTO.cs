using System.Collections.Generic;

namespace DeliverIT.Services.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public List<string> Parcels { get; set; } = new List<string>(); //TODO: ParcelDTO

    }
}
