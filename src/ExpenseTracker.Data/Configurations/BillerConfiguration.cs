using ExpenseTracker.Entities;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Metadata.Internal;

namespace ExpenseTracker.Data.Configurations
{
    public class BillerConfiguration : EntityBaseConfiguration<Biller>
    {
        public override void Map(EntityTypeBuilder<Biller> builder)
        {
            base.Map(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Address).HasMaxLength(400);
        }
    }
}
