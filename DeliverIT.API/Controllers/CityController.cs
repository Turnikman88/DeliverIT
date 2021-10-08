using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cs;
        private readonly IAuthenticationService _auth;

        public CityController(ICityService cs, IAuthenticationService auth)
        {
            this._cs = cs;
            this._auth = auth;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesAsync([FromHeader] string authorization)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var cities = await _cs.GetAsync();
            return this.Ok(cities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<City>> GetCityByIdAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var city = await _cs.GetCityByIdAsync(id);
            if (city is null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<City>> GetCityByNameAsync([FromHeader] string authorization, string name)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            var city = await _cs.GetCityByNameAsync(name);
            if (city == null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CityDTO>> CreateCityAsync([FromHeader] string authorization, CityDTO obj) //TODO Fix
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
        public async Task<ActionResult<CityDTO>> UpdateCityAsync([FromHeader] string authorization, int id, CityDTO obj)
        {
            if (!_auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }
            
            if (obj is null || await _cs.GetCityByIdAsync(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this._cs.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<City>> DeleteCityAsync([FromHeader] string authorization, int id)
        {
            if (!_auth.FindEmployee(authorization))
              return this.Unauthorized();
            
            if (await _cs.GetCityByIdAsync(id) is null)
              return this.BadRequest();
              
            return this.Ok(await this._cs.DeleteAsync(id));
        }
    }
}
