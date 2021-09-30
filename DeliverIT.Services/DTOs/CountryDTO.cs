namespace DeliverIT.Services.DTOs
{
    public class CountryDTO
    {
        public CountryDTO()
        {
            //CitiesDTO = new HashSet<CityDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<CityDTO> CitiesDTO { get; set; }
    }
}
