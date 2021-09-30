namespace DeliverIT.Services.DTOs
{
    public class CityDTO
    {
        public CityDTO()
        {
            //Addresses = new HashSet<AddressDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        //public virtual ICollection<AddressDTO> Addresses { get; set; }
    }
}