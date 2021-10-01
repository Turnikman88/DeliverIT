using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasIndex(e => e.CityId, "IX_Addresses_CityId");

            builder.Property(e => e.StreetName).IsRequired();

            builder.HasOne(d => d.City)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
