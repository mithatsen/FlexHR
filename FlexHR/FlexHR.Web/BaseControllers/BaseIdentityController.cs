using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.BaseControllers
{
    public class BaseIdentityController:Controller
    {
        protected readonly UserManager<AppUser> _userManager;
        public BaseIdentityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> IsAuthority(int id)
        {
           var user= await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.StaffId==id || User.IsInRole("Manager"))
            {
                return true;
            }
            return false;
        }
        protected async Task<AppUser> ActiveUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
