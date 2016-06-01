using ExpenseTracker.Web.Infrastructure.Core;
using FluentValidation;
using ExpenseTracker.Web.Infrastructure.Validators;

namespace ExpenseTracker.Web.ViewModels
{
    public class LoginViewModel : BaseViewModel<LoginViewModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        AbstractValidator<LoginViewModel> _validator;

        protected override AbstractValidator<LoginViewModel> Validator
        {
            get
            {
                if (_validator == null)
                    _validator = new LoginViewModelValidator();
                return _validator;
            }
        }

        public override LoginViewModel GetInstance()
        {
            return this;
        }
    }
}
