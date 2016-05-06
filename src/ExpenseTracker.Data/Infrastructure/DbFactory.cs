using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ExpenseTrackerContext dbContext;
        DbContextOptions contextOptions;
        public DbFactory(DbContextOptions contextOptions)
        {
            this.contextOptions = contextOptions;
        }

        public ExpenseTrackerContext Init()
        {
            return dbContext ?? (dbContext = new ExpenseTrackerContext(contextOptions));
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
