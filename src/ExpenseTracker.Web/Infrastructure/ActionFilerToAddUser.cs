using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using ExpenseTracker.Web.Controllers;

namespace ExpenseTracker.Web.Infrastructure
{
    public class ActionFilerToAddUser : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "POST" && context.Controller.GetType() != typeof(AccountController))
            {
                var claimsPrincipal = context.HttpContext.User;
                var UserName = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                foreach (var item in context.ActionArguments)
                {
                    var propertyInfo = item.Value.GetType().GetProperty("CreatedBy");
                    propertyInfo?.SetValue(item.Value, UserName, null);
                }

            }
        }
    }
}
