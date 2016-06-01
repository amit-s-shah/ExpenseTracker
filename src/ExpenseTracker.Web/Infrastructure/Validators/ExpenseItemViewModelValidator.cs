using ExpenseTracker.Web.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Infrastructure.Validators
{
    public class ExpenseItemViewModelValidator : AbstractValidator<ExpenseItemViewModel>
    {
        public ExpenseItemViewModelValidator()
        {
        }
    }
}
