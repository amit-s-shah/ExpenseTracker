using System;

namespace ExpenseTracker.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ExpenseTrackerContext Init();
    }
}
