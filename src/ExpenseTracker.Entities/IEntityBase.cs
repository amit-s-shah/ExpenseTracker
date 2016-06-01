using System;

namespace ExpenseTracker.Entities
{
    public interface IEntityBase
    {
        int ID { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
}
