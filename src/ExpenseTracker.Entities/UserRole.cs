namespace ExpenseTracker.Entities
{
    public class UserRole : EntityBase
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
