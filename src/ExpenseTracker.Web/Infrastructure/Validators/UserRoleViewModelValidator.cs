using ExpenseTracker.Web.ViewModels;
using FluentValidation;

namespace Infrastructure.Validators
{
    class UserRoleViewModelValidator : AbstractValidator<UserRoleViewModel>
    {
        public UserRoleViewModelValidator()
        {

        }
    }
}