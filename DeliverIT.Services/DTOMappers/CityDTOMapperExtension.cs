using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Services.DTOMappers
{
    public static class CityDTOMapperExtension
    {
        public static CityDTO GetDTO(this City city)
        {
            return new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = city.Country.Name
            };
        }

        public static City GetEntity(this CityDTO city)
        {
            return new City
            {
                Id = city.Id,
                Name = city.Name,
                Country = new Country
                {
                    Name = city.CountryName //TODO: if existing?
                }
            };
        }
    }
}
