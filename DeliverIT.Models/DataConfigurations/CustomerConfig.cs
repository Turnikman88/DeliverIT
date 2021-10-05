using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(e => e.AddressId, "IX_Customers_AddressId");

            builder.HasOne(d => d.Address)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId);
        }
    }
}
