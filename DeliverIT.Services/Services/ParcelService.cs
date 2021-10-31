using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class ParcelService : IParcelService
    {
        private readonly DeliverITDBContext _db;
        public ParcelService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<ParcelDTO>> ListCustomerIncomingParcelsAsync(int id)
        {
            return await this._db.Parcels
                .Include(x => x.Category)
                .Include(x => x.Shipment.Status)
                .Where(x => x.CustomerId == id && x.Shipment.StatusId == 2)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<ParcelDTO> DeleteAsync(int id)
        {
            var parcel = await _db.Parcels
                .Include(x => x.Category)
                .Include(x => x.Shipment.Status)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.PARCEL_NOT_FOUND);

            var parcelDTO = parcel.GetDTO();

            parcel.DeletedOn = DateTime.Now;
            _db.Parcels.Remove(parcel);
            await _db.SaveChangesAsync();
            return parcelDTO;
        }

        public async Task<IEnumerable<ParcelDTO>> GetAsync()
        {
            return await this._db.Parcels
                .Include(x => x.Category)
                .Include(x => x.Shipment.Status)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<ParcelDTO> PostAsync(ParcelDTO obj)
        {
            ParcelDTO result = null;
            var newParcel = obj.GetEntity();
            var deleteParcel = await this._db.Parcels.IgnoreQueryFilters()
                .Include(x => x.Category)
                .Include(x => x.Shipment.Status)
                .FirstOrDefaultAsync(x => x.CustomerId == newParcel.CustomerId
                && x.ShipmentId == newParcel.ShipmentId
                && x.WareHouseId == newParcel.WareHouseId
                && x.CategoryId == newParcel.CategoryId
                && x.Weight == newParcel.Weight
                && x.DeliverToAddress == newParcel.DeliverToAddress
                && x.IsDeleted == true);

            if (deleteParcel == null)
            {
                if (await IsInvalidParcel(obj.CustomerId, obj.ShipmentId, obj.WareHouseId, obj.CategoryId))
                {
                    throw new AppException(Constants.INVALID_ID); //ToDo: Add better exception message
                }

                await this._db.Parcels.AddAsync(newParcel);
                await this._db.SaveChangesAsync();
                newParcel = await this._db.Parcels
                    .Include(x => x.Category)
                    .Include(x => x.Shipment.Status)
                    .FirstOrDefaultAsync(x => x.Id == newParcel.Id);
                result = newParcel.GetDTO();
            }
            else
            {
                deleteParcel.IsDeleted = false;
                deleteParcel.DeletedOn = null;
                await this._db.SaveChangesAsync();
                result = deleteParcel.GetDTO();
            }

            return result;
        }

        public async Task<ParcelDTO> UpdateAsync(int id, ParcelDTO obj)
        {
            var parcel = await _db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.PARCEL_NOT_FOUND);

            if (obj is null || obj.CustomerId <= 0 || obj.ShipmentId <= 0
               || obj.WareHouseId <= 0 || obj.CategoryId <= 0
               || obj.Weight < 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }
            if (await IsInvalidParcel(obj.CustomerId, obj.ShipmentId, obj.WareHouseId, obj.CategoryId))
            {
                throw new AppException(Constants.INVALID_ID); //ToDo: Add better exception message
            }

            parcel.CustomerId = obj.CustomerId;
            parcel.ShipmentId = obj.ShipmentId;
            parcel.WareHouseId = obj.WareHouseId;
            parcel.CategoryId = obj.CategoryId;
            parcel.Weight = obj.Weight;
            parcel.DeliverToAddress = obj.DeliverToAddress;

            var parcelDTO = parcel.GetDTO();
            await _db.SaveChangesAsync();

            return parcelDTO;
        }
        public async Task<ParcelDTO> GetParcelByIdAsync(int id)
        {
            var parcel = await _db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.PARCEL_NOT_FOUND);

            return parcel.GetDTO();
        }

        public async Task<IEnumerable<ParcelDTO>> SortByWeightAsync()
        {
            return await this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).OrderBy(x => x.Weight)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> SortByArrivalDateAsync()
        {
            return await this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).OrderBy(x => x.Shipment.ArrivalDate)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> SortByWeightAndArrivalDateAsync()
        {
            return await this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status)
                .OrderBy(x => x.Weight).ThenBy(x => x.Shipment.ArrivalDate)
                .Select(x => x.GetDTO()).ToListAsync();
        }
        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerIdAsync(int id)
        {
            return await this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status)
                .Where(x => x.CustomerId == id).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> GetSortedParcelsByCustomerIdAsync(int id)
        {
            return await this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).Where(x => x.CustomerId == id)
                .OrderBy(x => x.Shipment.Status).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> MultiFilterAsync(int? id, int? customerId, int? shipmentId,
            int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            var result = this._db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).AsQueryable();

            if (id.HasValue)
            {
                result = result.Where(x => x.Id == id);
            }
            if (customerId.HasValue)
            {
                result = result.Where(x => x.CustomerId == customerId);
            }
            if (shipmentId.HasValue)
            {
                result = result.Where(x => x.ShipmentId == shipmentId);
            }
            if (warehouseId.HasValue)
            {
                result = result.Where(x => x.WareHouseId == warehouseId);
            }
            if (categoryId.HasValue)
            {
                result = result.Where(x => x.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                result = result.Where(x => x.Category.Name.Contains(categoryName));
            }
            if (minWeight.HasValue)
            {
                result = result.Where(x => x.Weight >= minWeight);
            }
            if (maxWeight.HasValue)
            {
                result = result.Where(x => x.Weight <= maxWeight);
            }

            return await result.Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetShipmentStatusAsync(int customerId)
        {
            return await this._db.Parcels.Where(x => x.CustomerId == customerId)
                .Select(x => $"Id: {x.Id}, {x.Shipment.Status.Name}").ToListAsync();
        }

        public async Task<ParcelDTO> ChangeDeliverLocationAsync(int id)
        {
            var parcel = await _db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.PARCEL_NOT_FOUND);

            if (parcel.Shipment.StatusId == 3)
            {
                throw new AppException(Constants.SHIPMENT_ALREADY_ARRIVED);
            }
            else
            {
                parcel.DeliverToAddress = parcel.DeliverToAddress == true ? false : true;
            }

            var parcelDTO = parcel.GetDTO();
            await _db.SaveChangesAsync();

            return parcelDTO;
        }
        private async Task<bool> IsInvalidParcel(int customerId, int shipmentId, int? warehouseId, int categoryId)
        {
            var customers = await _db.Customers.AnyAsync(x => x.Id == customerId);
            var shipments = await _db.Shipments.AnyAsync(x => x.Id == shipmentId);
            var warehouses = await _db.WareHouses.AnyAsync(x => x.Id == warehouseId);
            var categories = await _db.Categories.AnyAsync(x => x.Id == categoryId);
            return !(customers && shipments && warehouses && categories);
        }
    }
}
