using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ExpenseTrackerContext Init();
    }
}
