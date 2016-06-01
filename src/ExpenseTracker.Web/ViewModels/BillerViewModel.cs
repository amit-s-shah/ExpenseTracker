using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Web.Infrastructure.Validators;

namespace ExpenseTracker.Web.ViewModels
{
    public class BillerViewModel : IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new BillerViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(error => new ValidationResult(error.ErrorMessage, new[] { error.PropertyName }));
        }
    }
}
