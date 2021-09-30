using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DeliverIT.Models.DataConfigurations
{
    class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasIndex(e => e.CountryId, "IX_Cities_CountryId");
            
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId);
            builder.HasIndex(x => new { x.Name, x.CountryId });
        }
    }
}
