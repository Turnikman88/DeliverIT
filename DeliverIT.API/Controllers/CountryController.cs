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
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync()
        {
            var countries = await cs.GetAsync();
            return this.Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> CreateCountryAsync(CountryDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }
            
            return this.Created("Get", await this.cs.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> UpdateCountryAsync(int id, CountryDTO obj)
        {
            if (obj is null || await cs.GetCountryByIdAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CountryDTO>> DeleteCountryAsync(int id)
        {
            if (await cs.GetCountryByIdAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this.cs.DeleteAsync(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CountryDTO>> GetCountryByIdAsync(int id)
        {
            var country = await cs.GetCountryByIdAsync(id);
            if (country is null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CountryDTO>> GetCountryByNameAsync(string name)
        {
            var country = await cs.GetCountryByNameAsync(name);
            if (country == null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePartAsync(string part)
        {
            var countries = await cs.GetCountriesByPartNameAsync(part);
            if (countries == null)
            {
                return this.NotFound();
            }
            return this.Ok(countries);
        }
    }
}
