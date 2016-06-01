using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class InvoiceConfiguration : EntityBaseConfiguration<Invoice>
    {
        public override void Map(EntityTypeBuilder<Invoice> builder)
        {
            base.Map(builder);
            builder.Property(inv => inv.InvoiceCopy).IsRequired();
            builder.Property(inv => inv.Format).IsRequired().HasMaxLength(10);
        }
    }
}
