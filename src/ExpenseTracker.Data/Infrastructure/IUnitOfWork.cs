using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Infrastructure
{
    interface IUnitOfWork
    {
        void Commit();
    }
}
