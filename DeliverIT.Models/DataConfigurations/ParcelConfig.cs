using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class ParcelConfig : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.HasIndex(e => e.CategoryId, "IX_Parcels_CategoryId");

            builder.HasIndex(e => e.CustomerId, "IX_Parcels_CustomerId");

            builder.HasIndex(e => e.WareHouseId, "IX_Parcels_WareHouseId");

            builder.HasIndex(e => e.ShipmentId, "IX_Parcels_ShipmentId");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Parcels)
                .HasForeignKey(d => d.CategoryId);

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Parcels)
                .HasForeignKey(d => d.CustomerId);

            builder.HasOne(d => d.WareHouse)
                .WithMany(p => p.Parcels)
                .HasForeignKey(d => d.WareHouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Shipment)
                .WithMany(p => p.Parcels)
                .HasForeignKey(d => d.ShipmentId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
