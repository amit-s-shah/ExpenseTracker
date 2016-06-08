using ExpenseTracker.Web.Infrastructure.Core;
using System.Collections.Generic;
using FluentValidation;
using Infrastructure.Validators;

namespace ExpenseTracker.Web.ViewModels
{
    public class UserRoleViewModel : BaseViewModel<UserRoleViewModel>
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsLocked { get; set; }

        public ICollection<RoleViewModal> UserRoles
        {
            get;
            set;
        }

        AbstractValidator<UserRoleViewModel> _validator;
        protected override AbstractValidator<UserRoleViewModel> Validator
        {
            get
            {
                if (_validator == null)
                    _validator = new UserRoleViewModelValidator();
                return _validator;
            }
        }

        public override UserRoleViewModel GetInstance()
        {
            return this;
        }
    }
}
