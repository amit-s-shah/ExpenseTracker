using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class PaymentMethodConfiguration : EntityBaseConfiguration<PaymentMethod>
    {
        public override void Map(EntityTypeBuilder<PaymentMethod> builder)
        {
            base.Map(builder);
            builder.Property(paymth => paymth.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(paymth => paymth.Name).IsUnique();
        }
    }
}
