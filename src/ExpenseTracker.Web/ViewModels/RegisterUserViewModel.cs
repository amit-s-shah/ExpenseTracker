using ExpenseTracker.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using ExpenseTracker.Web.Infrastructure.Validators;

namespace ExpenseTracker.Web.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel<RegisterUserViewModel>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        AbstractValidator<RegisterUserViewModel> _validator;

        protected override AbstractValidator<RegisterUserViewModel> Validator
        {
            get
            {
                if (_validator == null)
                    _validator = new RegisterUserViewModelValidator();
                return _validator;
            }
        }

        public override RegisterUserViewModel GetInstance()
        {
            return this;
        }
    }
}
