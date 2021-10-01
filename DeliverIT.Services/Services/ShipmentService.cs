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
    public class ShipmentService : IShipmentService
    {
        private readonly DeliverITDBContext db;

        public ShipmentService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<ShipmentDTO> Delete(int id)
        {
            var shipment = await db.Shipments.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
            var shipmentDTO = shipment.GetDTO();
            db.Shipments.Remove(shipment);
            await db.SaveChangesAsync();
            return shipmentDTO;
        }

        public async Task<IEnumerable<ShipmentDTO>> Get()
        {
            return await db.Shipments.Include(x => x.Status).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<ShipmentDTO> Post(ShipmentDTO obj)
        {
            await db.Shipments.AddAsync(obj.GetEntity());
            await db.SaveChangesAsync();

            return obj; //ToDo: how to get the Id?? Nothing is unique
        }

        public async Task<ShipmentDTO> Update(int id, ShipmentDTO obj)
        {
            var shipment = await db.Shipments.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);

            shipment.ArrivalDate = obj.ArrivalDate;
            shipment.DepartureDate = obj.DepartureDate;
            shipment.DestinationWareHouseId = obj.DestinationWareHouseId;
            shipment.StatusId = obj.StatusId;
            var shipmentDTO = shipment.GetDTO();
            await db.SaveChangesAsync();
            return shipmentDTO;
        }
        public async Task<bool> ShipmentExists(int id)
        {
            var model = await db.Shipments.FirstOrDefaultAsync(x => x.Id == id);
            return model is null ? false : true;
        }
    }
}
