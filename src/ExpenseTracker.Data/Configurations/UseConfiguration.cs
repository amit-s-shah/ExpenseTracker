using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            base.Map(builder);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.HashedPassword).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Salt).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsLocked).IsRequired();
            builder.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.DateCreated).ValueGeneratedOnAdd();
        }
    }
}
