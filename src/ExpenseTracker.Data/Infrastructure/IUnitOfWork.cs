namespace ExpenseTracker.Data.Infrastructure
{
    public interface IUnitofWork
    {
        void Commit();
    }
}
