using DeliverIT.Services.DTOs;

namespace DeliverIT.Web.Models.Mappers
{
    public static class ParcelMapperExtension
    {
        public static ParcelDTO GetParcelDTO(this ParcelViewModel parcel)
        {
            return new ParcelDTO
            {
                CustomerId = parcel.CustomerId ?? 0,
                ShipmentId = parcel.ShipmentId ?? 0,
                WareHouseId = parcel.WareHouseId ?? 0,
                CategoryId = parcel.CategoryId ?? 1,
                DeliverToAddress = parcel.DeliverToAddress,
                Weight = parcel.Weight ?? 0
            };
        }
        public static ParcelViewModel GetParcelViewModel(this ParcelDTO parcel)
        {
            return new ParcelViewModel
            {
                CustomerId = parcel.CustomerId,
                ShipmentId = parcel.ShipmentId,
                WareHouseId = parcel.WareHouseId,
                CategoryId = parcel.CategoryId,
                DeliverToAddress = parcel.DeliverToAddress,
                Weight = parcel.Weight 
            };
        }
    }
}
