using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class ShipmentConfig : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasIndex(e => e.DestinationWareHouseId, "IX_Shipments_DestinationWareHouseId");

            builder.HasIndex(e => e.OriginWareHouseId, "IX_Shipments_OriginWareHouseId");

            builder.HasIndex(e => e.StatusId, "IX_Shipments_StatusId");

            builder.HasOne(d => d.DestinationWareHouse)
                .WithMany(prop => prop.ShipmentDestinationWareHouses)
                .HasForeignKey(d => d.DestinationWareHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.OriginWareHouse)
                    .WithMany(p => p.ShipmentOriginWareHouses)
                    .HasForeignKey(d => d.OriginWareHouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Status)
                .WithMany(p => p.Shipments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
