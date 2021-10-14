using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;

namespace DeliverIT.Services.DTOMappers
{
    public static class ParcelDTOMapperExtension
    {
        public static ParcelDTO GetDTO(this Parcel parcel)
        {
            if (parcel is null || parcel.CustomerId <= 0 || parcel.ShipmentId <= 0
                || parcel.WareHouseId <= 0 || parcel.CategoryId <= 0
                || parcel.Weight < 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

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
            if (parcel is null || parcel.CustomerId <= 0 || parcel.ShipmentId <= 0
                || parcel.WareHouseId <= 0 || parcel.CategoryId <= 0
                || parcel.Weight < 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

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
