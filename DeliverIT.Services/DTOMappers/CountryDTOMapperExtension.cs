using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Services.DTOMappers
{
    public static class CountryDTOMapperExtension
    {
        public static CountryDTO GetDTO(this Country country)
        {
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name
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
