using ExpenseTracker.Entities;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Metadata.Internal;

namespace ExpenseTracker.Data.Configurations
{
    public class EntityBaseConfiguration<T> where T : class, IEntityBase
    {

        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.CreatedBy).HasMaxLength(20);
            builder.Property(x => x.CreatedDate).ValueGeneratedOnAddOrUpdate();
        }
    }
}
