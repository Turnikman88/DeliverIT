using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class ShippmentService : IShippmentService
    {
        private readonly DeliverITDBContext db;

        public ShippmentService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<ShipmentDTO> Delete(int id)
        {
            var shippment = await db.Shipments.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
            var shippmentDTO = shippment.GetDTO();
            db.Shipments.Remove(shippment);
            await db.SaveChangesAsync();
            return shippmentDTO;
        }

        public async Task<IEnumerable<ShipmentDTO>> Get()
        {
            return await db.Shipments.Include(x => x.Status).Select(x => x.GetDTO()).ToListAsync();
        }

        public Task<ShipmentDTO> Post(ShipmentDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<ShipmentDTO> Update(int id, ShipmentDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
