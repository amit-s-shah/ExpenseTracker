using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }
    }
}
