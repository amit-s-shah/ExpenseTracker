using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IDbFactory dbFactory;
        ExpenseTrackerContext dbContext;
        private DbContextOptions contextOptions;
        public UnitOfWork(IDbFactory dbFactory, DbContextOptions contextOptions)
        {
            this.dbFactory = dbFactory;
            this.contextOptions = contextOptions;
        }

        public ExpenseTrackerContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit() { DbContext.SaveChanges(); }
    }
}
