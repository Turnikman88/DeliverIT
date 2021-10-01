using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DeliverIT.Models.DataConfigurations
{
    class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(e => e.Name).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
