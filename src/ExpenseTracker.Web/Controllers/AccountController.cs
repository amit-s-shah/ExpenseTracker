using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Web.Infrastructure.Core;
using ExpenseTracker.Data.Infrastructure;
using ExpenseTracker.Services.Infrastructure;
using ExpenseTracker.Web.ViewModels;
using AutoMapper;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseTracker.Web.Controllers
{
    public class AccountController : ExpenseTrackerCtrlBase
    {
        IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService, IUnitofWork unitOfWork) : base(unitOfWork)
        {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        [HttpPost]
        public UserRoleViewModel Register([FromBody] RegisterUserViewModel RegisterUser)
        {
            int[] role = new int[0];
            var user = _membershipService.CreateUser(RegisterUser.UserName, RegisterUser.Email, RegisterUser.Password, role);
            return Mapper.Map<UserRoleViewModel>(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> LoginAsync([FromBody]LoginViewModel user)
        {
            bool result;
            if (ModelState.IsValid)
            {
                MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);
                if (_userContext.User == null)
                {
                    result = false;
                }
                else
                {
                    StringBuilder roles = new StringBuilder();
                    _userContext.User.UserRoles?.ToList().ForEach(userRole => roles.Append(userRole.RoleId).Append(","));
                    var identities = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, _userContext.User.Username),
                            new Claim(ClaimTypes.Email, _userContext.User.Email),
                            new Claim(ClaimTypes.Role, roles.ToString())
                         }, "ExpenseTrackerAuthKey");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identities);
                    Request.HttpContext.User = principal;
                    await Request.HttpContext.Authentication.SignInAsync("ExpenseTrackerAuthKey", principal);
                    //task.Wait();
                    result = true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        [HttpPost]
        public async void logoutAsync()
        {
            await Request.HttpContext.Authentication.SignOutAsync("ExpenseTrackerAuthKey");
        }

    }
}
