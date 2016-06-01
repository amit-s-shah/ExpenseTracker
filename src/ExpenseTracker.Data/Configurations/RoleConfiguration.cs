using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public override void Map(EntityTypeBuilder<Role> builder)
        {
            base.Map(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
