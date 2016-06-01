using ExpenseTracker.Data.Repositories;
using ExpenseTracker.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Infrastructure
{
    public class ValidateCookiePrincipal
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            // Pull database from registered DI services.
            var serviceProvider  = context.HttpContext.RequestServices;
            var userRepository = (EntityBaseRepository<User>)serviceProvider.GetService(typeof(EntityBaseRepository<User>));
            var userPrincipal = context.Principal;

            // Look for the last changed claim.
            //string lastChanged;
            //lastChanged = (from c in userPrincipal.Claims
            //               where c.Type == "LastUpdated"
            //               select c.Value).FirstOrDefault();

            var UserName = userPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            if (userRepository?.FindBy(user => user.Username == UserName).Count() == 0)
            {
                context.RejectPrincipal();
                await context.HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            }
        }
    }
}
