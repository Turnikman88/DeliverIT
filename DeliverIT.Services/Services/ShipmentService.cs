using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly DeliverITDBContext _db;

        public ShipmentService(DeliverITDBContext db)
        {
            this._db = db;
        }


        public async Task<ShipmentDTO> DeleteAsync(int id)
        {
            var shipment = await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels).FirstOrDefaultAsync(x => x.Id == id);
            var shipmentDTO = shipment.GetDTO();

            shipment.DeletedOn = DateTime.Now;
            _db.Shipments.Remove(shipment);
            await _db.SaveChangesAsync();
            return shipmentDTO;
        }

        public async Task<IEnumerable<ShipmentDTO>> GetAsync()
        {
            return await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<ShipmentDTO> PostAsync(ShipmentDTO obj)
        {
            ShipmentDTO result = null;

            var newShipment = obj.GetEntity();
            var deleteShipment = await _db.Shipments.IgnoreQueryFilters().Include(x => x.Status).Include(x => x.Parcels)
                .FirstOrDefaultAsync(x => x.DepartureDate == newShipment.DepartureDate
                && x.ArrivalDate == newShipment.ArrivalDate
                && x.DestinationWareHouseId == newShipment.DestinationWareHouseId
                && x.OriginWareHouseId == newShipment.OriginWareHouseId
                && x.StatusId == newShipment.StatusId
                && x.IsDeleted == true);

            if (deleteShipment == null)
            {
                await _db.Shipments.AddAsync(newShipment);
                await _db.SaveChangesAsync();
                newShipment = await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels)
                    .FirstOrDefaultAsync(x => x.Id == newShipment.Id);

                result = newShipment.GetDTO();
            }
            else
            {
                deleteShipment.DeletedOn = null;
                deleteShipment.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deleteShipment.GetDTO();
            }

            return result;
        }

        public async Task<ShipmentDTO> UpdateAsync(int id, ShipmentDTO obj)
        {
            var shipment = await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels).FirstOrDefaultAsync(x => x.Id == id);

            shipment.ArrivalDate = obj.ArrivalDate;
            shipment.DepartureDate = obj.DepartureDate;
            shipment.DestinationWareHouseId = obj.DestinationWareHouseId;
            shipment.StatusId = obj.StatusId;
            var shipmentDTO = shipment.GetDTO();
            await _db.SaveChangesAsync();
            return shipmentDTO;
        }
        public async Task<bool> ShipmentExistsAsync(int id)
        {
            var shipment = await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels).FirstOrDefaultAsync(x => x.Id == id);
            return shipment is null ? false : true;
        }
        public async Task<ShipmentDTO> GetShipmentByIdAsync(int id)
        {
            var shipment = await _db.Shipments.Include(x => x.Status).Include(x => x.Parcels).FirstOrDefaultAsync(x => x.Id == id);

            return shipment.GetDTO();
        }
        public async Task<IEnumerable<ShipmentDTO>> FilterByDestinationWareHouseAsync(int id)
        {
            return await this._db.Shipments.Include(x => x.Status).Include(x => x.Parcels)
                .Where(x => x.DestinationWareHouseId == id).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByOriginWareHouseAsync(int id)
        {
            return await this._db.Shipments.Include(x => x.Status).Include(x => x.Parcels)
                .Where(x => x.OriginWareHouseId == id).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByCustomerIdAsync(int id)
        {
            var shipments = await this._db.Shipments.Include(x => x.Status).Include(x => x.Parcels.Where(y => y.CustomerId == id)).ToListAsync();
            var result = shipments.Where(x => x.Parcels.Count > 0).Select(x => x.GetDTO()).ToList();
               
            return result;
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByCustomerNameAsync(string name)
        {
            var shipments = await this._db.Shipments.Include(x => x.Status)
                .Include(x => x.Parcels.Where(y => y.Customer.FirstName.Contains(name) || y.Customer.LastName.Contains(name))).ToListAsync();

            var result = shipments.Where(x => x.Parcels.Count > 0).Select(x => x.GetDTO()).ToList();

            return result;
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByCustomerEmailAsync(string email)
        {
            var shipments = await this._db.Shipments.Include(x => x.Status)
                .Include(x => x.Parcels.Where(y => y.Customer.Email.Contains(email))).ToListAsync();

            var result = shipments.Where(x => x.Parcels.Count > 0).Select(x => x.GetDTO()).ToList();

            return result;
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByCustomerAddressAsync(string address)
        {
            var shipments = await this._db.Shipments.Include(x => x.Status)
                .Include(x => x.Parcels.Where(y => y.Customer.Address.StreetName.Contains(address))).ToListAsync();

            var result = shipments.Where(x => x.Parcels.Count > 0).Select(x => x.GetDTO()).ToList();

            return result;
        }

        public async Task<IEnumerable<ShipmentDTO>> FilterByStatusIdAsync(int id)
        {
            return await this._db.Shipments.Include(x => x.Status)
                .Include(x => x.Parcels).Where(x => x.StatusId == id).Select(x => x.GetDTO()).ToListAsync();            
        }
    }
}
