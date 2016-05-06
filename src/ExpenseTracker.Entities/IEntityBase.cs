using System;

namespace ExpenseTracker.Entities
{
    public interface IEntityBase
    {
        int ID { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime CreatedBy { get; set; }
    }
}
