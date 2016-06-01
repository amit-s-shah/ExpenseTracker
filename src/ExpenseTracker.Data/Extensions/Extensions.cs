using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Entities;
using System.Linq;

namespace ExpenseTracker.Data.Extensions
{
    public static class ExpenseTrackerDBExtensions
    {
        public static User GetSingleUserByName(this IEntityBaseRepository<User> userRepository, string userName)
        {
            return userRepository.GetAll().FirstOrDefault(u => u.Username == userName);
        }

        public static void EnsureSeedData(this ExpenseTrackerContext context)
        {
            if (!context.PaymentMethodtbl.Any())
            {
                context.PaymentMethodtbl.Add(new PaymentMethod() { CreatedBy = "sys", Name = "Cash" });
                context.PaymentMethodtbl.Add(new PaymentMethod() { CreatedBy = "sys", Name = "Credit Card" });
                context.PaymentMethodtbl.Add(new PaymentMethod() { CreatedBy = "sys", Name = "Netbanking" });
                context.PaymentMethodtbl.Add(new PaymentMethod() { CreatedBy = "sys", Name = "Cheque" });
                context.SaveChanges();
            }
        }
    }

}
