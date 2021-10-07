using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService ps;
        private readonly IAuthenticationService auth;

        public ParcelController(IParcelService ps, IAuthenticationService auth)
        {
            this.ps = ps;
            this.auth = auth;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync([FromHeader] string authorization, ParcelDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.BadRequest();
            }
            return this.Ok(await ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync([FromHeader] string authorization, int id, ParcelDTO obj)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (obj is null)
            {
                return this.NotFound();
            }
            return this.Ok(await ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync([FromHeader] string authorization, int id)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            if (!await ps.ParcelExistsAsync(id))
            {
                return this.NotFound();
            }
            return this.Ok(await ps.DeleteAsync(id));
        }       

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync([FromHeader] string authorization, int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync([FromHeader] string authorization)
        {
            if (!auth.FindEmployee(authorization))
            {
                return this.Unauthorized();
            }

            return this.Ok(await ps.SortByWeightAndArrivalDateAsync());
        }
    }
}
