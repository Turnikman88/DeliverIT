using DeliverIT.Models.DatabaseModels;
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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cs;

        public CityController(ICityService cs, IAuthenticationService auth)
        {
            this._cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            var cities = await _cs.GetAsync();
            return this.Ok(cities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<City>> GetCityByIdAsync(int id)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
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
        public async Task<ActionResult<City>> GetCityByNameAsync(string name)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
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
        public async Task<ActionResult<CityDTO>> CreateCityAsync(CityDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
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
        public async Task<ActionResult<CityDTO>> UpdateCityAsync(int id, CityDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
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
        public async Task<ActionResult<City>> DeleteCityAsync(int id)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            if (await _cs.GetCityByIdAsync(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this._cs.DeleteAsync(id));
        }
    }
}
