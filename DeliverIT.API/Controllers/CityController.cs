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
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            var cities = await cs.Get();
            return this.Ok(cities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var city = await cs.GetCityById(id);
            if (city is null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<City>> GetCityByName(string name)
        {
            var city = await cs.GetCityByName(name);
            if (city == null)
            {
                return this.NotFound();
            }
            return this.Ok(city);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CityDTO>> CreateCity(CityDTO obj) //TODO Fix
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
        public async Task<ActionResult<CityDTO>> UpdateCity(int id, CityDTO obj)
        {
            if (obj is null || await cs.GetCityById(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.cs.Update(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            if (await cs.GetCityById(id) is null)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.cs.Delete(id));
        }
    }
}
