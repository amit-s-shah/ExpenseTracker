using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configurations
{
    public class UserRoleConfiguration : EntityBaseConfiguration<UserRole>
    {
        public override void Map(EntityTypeBuilder<UserRole> builder)
        {
            base.Map(builder);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
        }
    }
}
