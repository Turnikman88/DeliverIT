using DeliverIT.API.Attributes;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _cs;

        public CountryController(ICountryService cs)
        {
            this._cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync()
        {
            return this.Ok(await _cs.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> CreateCountryAsync(CountryDTO obj)
        {
            return this.Created("Get", await this._cs.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> UpdateCountryAsync(int id, CountryDTO obj)
        {
            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> DeleteCountryAsync(int id)
        {
            return this.Ok(await this._cs.DeleteAsync(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> GetCountryByIdAsync(int id)
        {
            return this.Ok(await _cs.GetCountryByIdAsync(id));
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<CountryDTO>> GetCountryByNameAsync(string name)
        {
            return this.Ok(await _cs.GetCountryByNameAsync(name));
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePartAsync(string part)
        {
            return this.Ok(await _cs.GetCountriesByPartNameAsync(part));
        }
    }
}
