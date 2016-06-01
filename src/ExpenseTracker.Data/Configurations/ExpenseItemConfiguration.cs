using ExpenseTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseTracker.Data.Extensions;

namespace ExpenseTracker.Data.Configurations
{
    public class ExpenseItemConfiguration : EntityBaseConfiguration<ExpenseItem>
    {
        public override void Map(EntityTypeBuilder<ExpenseItem> builder)
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
        }
    }
}
