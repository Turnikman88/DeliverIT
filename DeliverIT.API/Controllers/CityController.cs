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
        private readonly ICityService cs;
        public CityController(ICityService cs)
        {
            this.cs = cs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesAsync()
        {
            var cities = await cs.GetAsync();
            return this.Ok(cities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<City>> GetCityByIdAsync(int id)
        {
            var city = await cs.GetCityByIdAsync(id);
            if (city is null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<City>> GetCityByNameAsync(string name)
        {
            var city = await cs.GetCityByNameAsync(name);
            if (city == null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CityDTO>> CreateCityAsync(CityDTO obj) //TODO Fix
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
        public async Task<ActionResult<CityDTO>> UpdateCityAsync(int id, CityDTO obj)
        {
            if (obj is null || await cs.GetCityByIdAsync(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.cs.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<City>> DeleteCityAsync(int id)
        {
            if (await cs.GetCityByIdAsync(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.cs.DeleteAsync(id));
        }
    }
}
