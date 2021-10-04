using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService ps;
        public ParcelController(IParcelService ps)
        {
            this.ps = ps;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync(int id)
        {
            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync()
        {
            return this.Ok(await ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync(ParcelDTO obj)
        {
            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync(int id, ParcelDTO obj)
        {
            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync(int id)
        {
            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.DeleteAsync(id));
        }

        [HttpGet("filter/weight/{criteria}/{weight}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByDestinationWareHouseAsync(string criteria, int weight)
        {
            if (criteria != "above" && criteria != "below")
            {
                return this.BadRequest();
            }
            return this.Ok(await ps.FilterByWeightAsync(criteria, weight));
        }
        

        [HttpGet("filter/customer/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync(int id)
        {
            return this.Ok(await ps.FilterByCustomerIdAsync(id));
        }
        
        [HttpGet("filter/customer/name/{name}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerNameAsync(string name)
        {
            return this.Ok(await ps.FilterByCustomerNameAsync(name));
        }
        
        [HttpGet("filter/customer/email/{email}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerEmailAsync(string email)
        {
            return this.Ok(await ps.FilterByCustomerEmailAsync(email));
        }
        
        [HttpGet("filter/customer/address/{address}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerAddressAsync(string address)
        {
            return this.Ok(await ps.FilterByCustomerAddressAsync(address));
        }

        [HttpGet("filter/warehouse/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByWareHouseIdAsync(int id)
        {
            return this.Ok(await ps.FilterByWareHouseAsyncId(id));
        }

        [HttpGet("filter/category/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCategoryIdAsync(int id)
        {
            return this.Ok(await ps.FilterByCategoryIdAsync(id));
        }

        [HttpGet("filter/category/name/{name}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCategoryNameAsync(string name)
        {
            return this.Ok(await ps.FilterByCategoryNameAsync(name));
        }

        [HttpGet("filter/customer-category")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerAndCategoryIdAsync(int categoryId, int customerId)
        {
            return this.Ok(await ps.FilterByCustomerAndCategoryIdAsync(categoryId, customerId));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync()
        {
            return this.Ok(await ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync()
        {
            return this.Ok(await ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync()
        {
            return this.Ok(await ps.SortByWeightAndArrivalDateAsync());
        }
    }
}
