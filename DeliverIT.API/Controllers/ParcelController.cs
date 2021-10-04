using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
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
    }
}
