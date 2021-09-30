using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System.Linq;

namespace DeliverIT.Services.DTOMappers
{
    public static class CountryDTOMapperExtension
    {
        public static CountryDTO GetDTO(this Country country)
        {
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                Cities = country.Cities.Select(c=>c.Name).ToList()
            };
        }

        public static Country GetEntity(this CountryDTO country)
        {
            return new Country
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
