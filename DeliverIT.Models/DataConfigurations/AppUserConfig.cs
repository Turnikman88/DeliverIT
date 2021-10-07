using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(e => e.Password).IsRequired();
            builder.HasCheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");

            builder.Property(e => e.Email).IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
