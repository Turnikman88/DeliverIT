using System.Collections.Generic;

namespace DeliverIT.Services.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public List<string> Addresses = new List<string>();
    }
}