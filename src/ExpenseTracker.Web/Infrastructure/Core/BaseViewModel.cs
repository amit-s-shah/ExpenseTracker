//using FluentValidation;
using ExpenseTracker.Web.Infrastructure.Validators;
//using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Infrastructure.Core
{
    public abstract class BaseViewModel<T> : IValidatableObject
    {
        protected abstract FluentValidation.AbstractValidator<T> Validator { get; }

        //public BaseViewModel(FluentValidation.AbstractValidator<T> validator)
        //{
        //    _validator = validator;
        //}

        public abstract T GetInstance();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //var validator = new BillerViewModelValidator();
            var result = Validator.Validate(GetInstance());
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
