using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DeliverIT.Models.DataConfigurations
{
    class WareHouseConfig : IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WareHouse> builder)
        {
            builder.HasIndex(e => e.AddressId, "IX_WareHouses_AddressId").IsUnique();

            builder.HasOne(d => d.Address)
                .WithMany(p => p.WareHouses)
                .HasForeignKey(d => d.AddressId);
        }
    }
}
