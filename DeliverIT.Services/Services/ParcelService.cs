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

        public async Task<IEnumerable<ParcelDTO>> FilterByWeightAsync(string criteria, double weight)
        {
            if (criteria == "below")
            {
                return await this.db.Parcels.Where(x => x.Weight <= weight).Include(x => x.Category)
                    .Select(x => x.GetDTO()).ToListAsync();
            }
            else 
            {
                return await this.db.Parcels.Where(x => x.Weight >= weight).Include(x => x.Category)
                    .Select(x => x.GetDTO()).ToListAsync();
            }
        }

        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerIdAsync(int id)
        {
            return await this.db.Parcels.Include(x => x.Category).Where(x => x.CustomerId == id).Select(x => x.GetDTO()).ToListAsync();            
        }

        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerNameAsync(string name)
        {
            return await this.db.Parcels.Include(x => x.Category)
                .Where(x => x.Customer.FirstName.Contains(name) || x.Customer.LastName.Contains(name))
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerEmailAsync(string email)
        {
            return await this.db.Parcels.Include(x => x.Category).Where(x => x.Customer.Email.Contains(email))
                .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<ParcelDTO>> FilterByCustomerAddressAsync(string address)
        {
            return await this.db.Parcels.Include(x => x.Category).Where(x => x.Customer.Address.StreetName.Contains(address))
               .Select(x => x.GetDTO()).ToListAsync();
        }

        public Task<IEnumerable<ParcelDTO>> FilterByWareHouseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParcelDTO>> FilterByCategoryIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParcelDTO>> FilterByCategoryNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}