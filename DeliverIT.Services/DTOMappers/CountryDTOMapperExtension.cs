using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using System.Linq;

namespace DeliverIT.Services.DTOMappers
{
    public static class CountryDTOMapperExtension
    {
        public static CountryDTO GetDTO(this Country country)
        {
            if (country is null || string.IsNullOrEmpty(country.Name) || country.Cities == null)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                Cities = country.Cities.Select(c => c.Name).ToList()
            };
        }

        public static Country GetEntity(this CountryDTO country)
        {
            if (country is null || string.IsNullOrEmpty(country.Name))
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }
            return new Country
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
