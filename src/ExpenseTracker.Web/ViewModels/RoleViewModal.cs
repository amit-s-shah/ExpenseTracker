using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.ViewModels
{
    public class RoleViewModal : IValidatableObject
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
