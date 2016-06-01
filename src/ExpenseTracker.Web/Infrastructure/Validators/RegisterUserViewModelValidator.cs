using ExpenseTracker.Web.ViewModels;
using FluentValidation;

namespace ExpenseTracker.Web.Infrastructure.Validators
{
    public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
    {
        public RegisterUserViewModelValidator()
        {
            RuleFor(user => user.Email).EmailAddress().Length(3, 50).WithMessage("Invalid Email address");
            RuleFor(user => user.UserName).Length(3, 60).WithMessage("User name length must be in between 3 to 60");
        }
    }
}
