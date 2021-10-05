using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DeliverIT.Models.DataConfigurations
{
    class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(e => e.AddressId, "IX_Employees_AddressId");

            builder.HasOne(d => d.Address)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddressId);
        }
    }
}
