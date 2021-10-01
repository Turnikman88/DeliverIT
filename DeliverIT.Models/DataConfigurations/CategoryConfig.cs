using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Models.DataConfigurations
{
    class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Name).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
