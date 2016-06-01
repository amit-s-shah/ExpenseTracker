using ExpenseTracker.Data.Configurations;
using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        #region Entity Set
        public DbSet<Biller> BillerTbl { get; set; }
        public DbSet<Category> CategoryTbl { get; set; }
        public DbSet<PaymentMethod> PaymentMethodtbl { get; set; }
        public DbSet<ExpenseItem> ExpenseItemtbl { get; set; }
        public DbSet<ReOccurringExpenseItem> ReOccurringExpenseItemtbl { get; set; }
        public DbSet<Invoice> Invoicetbl { get; set; }


        public DbSet<User> UserTbl { get; set; }
        public DbSet<Role> RoleTbl { get; set; }
        public DbSet<UserRole> UserRoleTbl { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            (new BillerConfiguration()).Map(modelBuilder.Entity<Biller>());
            (new CategoryConfiguration()).Map(modelBuilder.Entity<Category>());

            (new UserConfiguration()).Map(modelBuilder.Entity<User>());
            (new RoleConfiguration()).Map(modelBuilder.Entity<Role>());
            (new UserRoleConfiguration()).Map(modelBuilder.Entity<UserRole>());
            (new PaymentMethodConfiguration()).Map(modelBuilder.Entity<PaymentMethod>());
            (new ExpenseItemConfiguration()).Map(modelBuilder.Entity<ExpenseItem>());
            (new ReOccurringExpenseItemConfiguration()).Map(modelBuilder.Entity<ReOccurringExpenseItem>());
            (new InvoiceConfiguration()).Map(modelBuilder.Entity<Invoice>());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
        }

    }
}
