using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseTracker.Data.Extensions;

namespace ExpenseTracker.Data.Configurations
{
    public class ReOccurringExpenseItemConfiguration : EntityBaseConfiguration<ReOccurringExpenseItem>
    {
        ExpenseItemConfiguration expenseItemConfiguration = new ExpenseItemConfiguration();

        public override void Map(EntityTypeBuilder<ReOccurringExpenseItem> builder)
        {
            base.Map(builder);

            builder.Property(item => item.Name).IsRequired().HasMaxLength(30);
            builder.Property(item => item.Description).HasMaxLength(100);
            builder.Property(item => item.Amount).IsRequired();
            builder.Property(item => item.PurchasedDate).IsRequired();

            builder.Property(item => item.CategoryId).IsRequired();

            builder.Property(item => item.BillerId).IsRequired();

            builder.Property(item => item.InvoiceId);

            builder.Property(item => item.PaymentMethodId).IsRequired();

            builder.Property(item => item.IsActive).IsRequired();
            builder.Property(item => item.Frequency).IsRequired();
        }
    }
}
