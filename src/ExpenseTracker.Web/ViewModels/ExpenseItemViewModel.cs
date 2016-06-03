using ExpenseTracker.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ExpenseTracker.Web.Infrastructure.Validators;

namespace ExpenseTracker.Web.ViewModels
{
    public class ExpenseItemViewModel : BaseViewModel<ExpenseItemViewModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public DateTime Purchaseddate { get; set; }

        public int CategoryId { get; set; }

        public int BillerId { get; set; }

        public int? InvoiceId { get; set; }

        public int? PaymentMethodId { get; set; }

        AbstractValidator<ExpenseItemViewModel> expenseItemViewModelValidator;
        protected override AbstractValidator<ExpenseItemViewModel> Validator
        {
            get
            {
                if (expenseItemViewModelValidator == null)
                    expenseItemViewModelValidator = new ExpenseItemViewModelValidator();
                return expenseItemViewModelValidator;
            }
        }

        public override ExpenseItemViewModel GetInstance()
        {
            return this;
        }
    }
}
