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
        private readonly ICountryService _cs;
        private readonly IAuthenticationService _auth;

        public CountryController(ICountryService cs, IAuthenticationService auth)
        {
            this._cs = cs;
            this._auth = auth;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var countries = await _cs.GetAsync();
            return this.Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CountryDTO>> CreateCountryAsync([FromHeader] string authorization, CountryDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.BadRequest();
            }
            
            return this.Created("Get", await this._cs.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CountryDTO>> UpdateCountryAsync([FromHeader] string authorization, int id, CountryDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null || await _cs.GetCountryByIdAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CountryDTO>> DeleteCountryAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (await _cs.GetCountryByIdAsync(id) is null)
            {
                return this.NotFound();
            }

            return this.Ok(await this._cs.DeleteAsync(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CountryDTO>> GetCountryByIdAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var country = await _cs.GetCountryByIdAsync(id);
            if (country is null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CountryDTO>> GetCountryByNameAsync([FromHeader] string authorization, string name)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var country = await _cs.GetCountryByNameAsync(name);
            if (country == null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountryByNamePartAsync([FromHeader] string authorization, string part)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var countries = await _cs.GetCountriesByPartNameAsync(part);
            if (countries == null)
            {
                return this.NotFound();
            }
            return this.Ok(countries);
        }
    }
}
