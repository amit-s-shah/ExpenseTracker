using ExpenseTracker.Data.Configurations;
using ExpenseTracker.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        #region Entity Set
        public DbSet<Biller> BillerTbl { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            (new BillerConfiguration()).Map(modelBuilder.Entity<Biller>());
        }

    }
}
