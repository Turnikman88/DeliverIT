using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService cs;
        public CountryController(ICountryService cs)
        {
            this.cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            var countries = await cs.Get();
            return this.Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> CreateCountry(CountryDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }
            await this.cs.Post(obj);

            return this.Created("Get", obj);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> UpdateCountry(int id, CountryDTO obj)
        {
            if (obj is null || await cs.GetCountryById(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.Update(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> DeleteCountry(int id)
        {
            if (await cs.GetCountryById(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.Delete(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CountryDTO>> GetCountryById(int id)
        {
            var country = await cs.GetCountryById(id);
            if (country is null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CountryDTO>> GetCountryByName(string name)
        {
            var country = await cs.GetCountryByName(name);
            if (country == null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePart(string part)
        {
            var countries = await cs.GetCountriesByPartName(part);
            if (countries == null)
            {
                return this.NotFound();
            }
            return this.Ok(countries);
        }
    }
}
