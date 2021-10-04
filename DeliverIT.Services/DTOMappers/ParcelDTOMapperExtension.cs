using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Services.DTOMappers
{
    public static class ParcelDTOMapperExtension
    {
        public static ParcelDTO GetDTO(this Parcel parcel)
        {
            return new ParcelDTO
            {
                Id = parcel.Id,
                CustomerId = parcel.CustomerId,
                ShipmentId = parcel.ShipmentId,
                WareHouseId = parcel.WareHouseId,
                CategoryId = parcel.CategoryId,
                CategoryName = parcel.Category.Name,
                Weight = parcel.Weight,
                DeliverToAddress = parcel.DeliverToAddress
            };
        }
        public static Parcel GetEntity(this ParcelDTO parcel)
        {
            return new Parcel
            {
                Id = parcel.Id,
                CustomerId = parcel.CustomerId,
                ShipmentId = parcel.ShipmentId,
                WareHouseId = parcel.WareHouseId,
                CategoryId = parcel.CategoryId,
                Weight = parcel.Weight,
                DeliverToAddress = parcel.DeliverToAddress
            };
        }
    }
}
