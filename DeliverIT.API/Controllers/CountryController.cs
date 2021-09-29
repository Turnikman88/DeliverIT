using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await cs.Get();            
            return this.Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Country>> CreateCountry(Country obj)
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
        [ProducesResponseType(404)]
        public async Task<ActionResult<Country>> UpdateCountry(int id, Country obj) //ToDo: need to make it dto
        {
            if (obj is null || await cs.GetCountryById(id) is null)
            {
                return this.NotFound();
            }
            
            return this.Ok(await this.cs.Update(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
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
        public ActionResult<Country> GetCountryById(int id)
        {
            var country = cs.GetCountryById(id);
            if (country is null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<Country> GetCountryByName(string name)
        {
            var country = cs.GetCountryByName(name);
            if (country == null)
            {
                return this.NotFound();
            }
            return this.Ok(country);
        }

        [HttpGet("partname/{part}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Country>> GetCountryByNamePart(string part)
        {
            var countries = cs.GetCountriesByPartName(part);
            if (countries == null)
            {
                return this.NotFound();
            }
            return this.Ok(countries);
        }
    }
}
