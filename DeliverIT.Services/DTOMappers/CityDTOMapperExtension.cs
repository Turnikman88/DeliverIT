using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System.Linq;

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
                CountryName = city.Country.Name,
                Addresses = city.Addresses.Select(x => x.StreetName).ToList() 
            };
        }

        public static City GetEntity(this CityDTO city)
        {
            return new City
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId
            };
        }
    }
}
