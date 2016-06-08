using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ExpenseTracker.Web.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace ExpenseTracker.Web.Infrastructure
{
    public class UserRoleViewModelBinder : IModelBinder //, DefaultModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            UserRoleViewModel model = new UserRoleViewModel();
            var claimsPrincipal = bindingContext.OperationBindingContext.HttpContext.User;
            model.UserName = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            model.Email = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.FieldName,model);
            return Task.FromResult(bindingContext.Result);
        }

    }


    public class UserRoleViewModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(UserRoleViewModel))
                return null;
            return new UserRoleViewModelBinder();
        }
    }
}
