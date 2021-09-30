using System.Collections.Generic;

namespace DeliverIT.Services.DTOs
{
    public class CountryDTO
    {  
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string> Cities = new List<string>();        
    }
}
