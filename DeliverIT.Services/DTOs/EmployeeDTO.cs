namespace DeliverIT.Services.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
