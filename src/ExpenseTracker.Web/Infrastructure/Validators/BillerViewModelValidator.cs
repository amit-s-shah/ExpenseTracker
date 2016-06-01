using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ExpenseTracker.Web.ViewModels;

namespace ExpenseTracker.Web.Infrastructure.Validators
{
    public class BillerViewModelValidator : AbstractValidator<BillerViewModel>
    {
        public BillerViewModelValidator()
        {
            RuleFor(biller => biller.Id).GreaterThan(0).WithMessage("Biller Id is blank");
            RuleFor(biller => biller.Name).NotEmpty().NotNull().WithMessage("Biller name cannot be blank");
            RuleFor(biller => biller.Name).Length(3, 60).WithMessage("Biller name lenght must be between 3 to 60");
            RuleFor(biller => biller.Address).Length(0, 400).WithMessage("Biller address cannot be more than 400");
        }
    }
}
