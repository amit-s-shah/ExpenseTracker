using ExpenseTracker.Data.Extensions;
using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class EntityBaseConfiguration<T> where T : class, IEntityBase
    {

        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.ID); ;
            builder.Property(x => x.CreatedBy).HasMaxLength(20);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.CreatedDate).ValueGeneratedOnAddOrUpdate();
        }
    }
}
