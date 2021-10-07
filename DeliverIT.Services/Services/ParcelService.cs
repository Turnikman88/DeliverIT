﻿using DeliverIT.Models;
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
    public class ParcelService : IParcelService
    {
        private readonly DeliverITDBContext db;
        public ParcelService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<ParcelDTO> DeleteAsync(int id)
        {
            var parcel = await db.Parcels.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            var parcelDTO = parcel.GetDTO();

            parcel.DeletedOn = DateTime.Now;
            db.Parcels.Remove(parcel);
            await db.SaveChangesAsync();
            return parcelDTO;
        }

        public async Task<IEnumerable<ParcelDTO>> GetAsync()
        {
            return await this.db.Parcels.Include(x => x.Category).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<ParcelDTO> PostAsync(ParcelDTO obj)
        {
            ParcelDTO result = null;
            var newParcel = obj.GetEntity();
            var deleteParcel = await this.db.Parcels.IgnoreQueryFilters().Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.CustomerId == newParcel.CustomerId
                && x.ShipmentId == newParcel.ShipmentId
                && x.WareHouseId == newParcel.WareHouseId
                && x.CategoryId == newParcel.CategoryId
                && x.Weight == newParcel.Weight 
                && x.DeliverToAddress == newParcel.DeliverToAddress
                && x.IsDeleted == true);
            if (deleteParcel == null)
            {
                await this.db.Parcels.AddAsync(newParcel);
                await this.db.SaveChangesAsync();
                newParcel = await this.db.Parcels.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == newParcel.Id);
                result = newParcel.GetDTO();
            }
            else
            {
                deleteParcel.IsDeleted = false;
                deleteParcel.DeletedOn = null;
                await this.db.SaveChangesAsync();
                result = deleteParcel.GetDTO();
            }
            return result;
        }

        public async Task<ParcelDTO> UpdateAsync(int id, ParcelDTO obj)
        {
            var parcel = await db.Parcels.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            parcel.CustomerId = obj.CustomerId;
            parcel.ShipmentId = obj.ShipmentId;
            parcel.WareHouseId = obj.WareHouseId;
            parcel.CategoryId = obj.CategoryId;
            parcel.Weight = obj.Weight;
            parcel.DeliverToAddress = obj.DeliverToAddress;

            var parcelDTO = parcel.GetDTO();
            await db.SaveChangesAsync();
            return parcelDTO;
        }
        public async Task<ParcelDTO> GetParcelByIdAsync(int id)
        {
            var parcel = await db.Parcels.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            return parcel.GetDTO();
        }
        public async Task<bool> ParcelExistsAsync(int id)
        {
            var parcel = await db.Parcels.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            return parcel is null ? false : true;
        }

        public async Task<IEnumerable<ParcelDTO>> SortByWeightAsync()
        {
            return await this.db.Parcels.Include(x => x.Category).OrderBy(x => x.Weight)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> SortByArrivalDateAsync()
        {
            return await this.db.Parcels.Include(x => x.Category).OrderBy(x => x.Shipment.ArrivalDate)
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> SortByWeightAndArrivalDateAsync()
        {
            return await this.db.Parcels.Include(x => x.Category).OrderBy(x => x.Weight).ThenBy(x => x.Shipment.ArrivalDate)
                .Select(x => x.GetDTO()).ToListAsync();
        }
        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerIdAsync(int id)
        {
            return await this.db.Parcels.Include(x => x.Category).Where(x => x.CustomerId == id).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> MultiFilterAsync(int? id, int? customerId, int? shipmentId,
            int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            var result = await this.db.Parcels.Include(x => x.Category).Select(x => x.GetDTO()).ToListAsync();

            if (id.HasValue)
            {
                result = result.FindAll(x => x.Id == id);
            }
            if (customerId.HasValue)
            {
                result = result.FindAll(x => x.CustomerId == customerId);
            }
            if (shipmentId.HasValue)
            {
                result = result.FindAll(x => x.ShipmentId == shipmentId);
            }
            if (warehouseId.HasValue)
            {
                result = result.FindAll(x => x.WareHouseId == warehouseId);
            }
            if (categoryId.HasValue)
            {
                result = result.FindAll(x => x.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                result = result.FindAll(x => x.CategoryName.Contains(categoryName));
            }
            if (minWeight.HasValue)
            {
                result = result.FindAll(x => x.Weight >= minWeight);
            }
            if (maxWeight.HasValue)
            {
                result = result.FindAll(x => x.Weight <= maxWeight);
            }          
            
            return result;
        }
        
        public async Task<IEnumerable<string>> GetShipmentStatusAsync(int customerId) // ToDo: search by parcel id?
        {
            return await this.db.Parcels.Where(x => x.CustomerId == customerId)
                .Select(x => $"Id: {x.Id}, {x.Shipment.Status.Name}").ToListAsync();
        }
        public async Task<ParcelDTO> ChangeDeliverLocationAsync(int id, string deliverToAddress)
        {
            var parcel = await db.Parcels.Include(x => x.Category).Include(x => x.Shipment.Status).FirstOrDefaultAsync(x => x.Id == id);

            if (parcel.Shipment.StatusId == 3)
            {
                //throw exception
            }
            else
            {
                parcel.DeliverToAddress = deliverToAddress == "yes" ? true : false;
            }

            var parcelDTO = parcel.GetDTO();
            await db.SaveChangesAsync();
            return parcelDTO;
        }
    }
}
