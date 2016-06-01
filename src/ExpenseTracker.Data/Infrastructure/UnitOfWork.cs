using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Infrastructure
{
    public class UnitOfWork : IUnitofWork
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
