using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class CategoryConfiguration : EntityBaseConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> builder)
        {
            base.Map(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description).HasMaxLength(400);
        }
    }
}
