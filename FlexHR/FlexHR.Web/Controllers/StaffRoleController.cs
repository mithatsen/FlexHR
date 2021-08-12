using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffRoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;
        public StaffRoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAppRoleService appRoleService, IAppUserService appUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appRoleService = appRoleService;
            _appUserService = appUserService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            var roles = _appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125 && x.IsActive == true).ToList();
            var permissions = _appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 126 && x.IsActive == true).ToList();
            var user = _appUserService.Get(x => x.StaffId == id && x.IsActive==true).FirstOrDefault();
            bool isUser = user != null ? true : false;
            if (user != null){
                var userAllRoles = _appUserService.GetAppRolesByStaffId(id);
                List<AppRole> userRoles = new List<AppRole>();
                List<AppRole> userPermissions = new List<AppRole>();
                foreach (var item in userAllRoles)
                {
                    if (item.AuthorizeTypeGeneralSubTypeId == 125)
                    {
                        userRoles.Add(item);
                    }
                    else
                    {
                        userPermissions.Add(item);
                    }
                }
                var model = new ListRolesWithSelectedStaffRole { Permissions = permissions, Roles = roles, UserRoles = userRoles, UserPermissions = userPermissions, IsUser = isUser };
                return View(model);
            }
            else
            {
                var model = new ListRolesWithSelectedStaffRole { Permissions = permissions, Roles = roles,IsUser= isUser,UserPermissions=new List<AppRole>(), UserRoles=new List<AppRole>() };
                return View(model);
            }
          
        }
        public async Task<IActionResult> AddRole(AddMultipleRoleToStaffDto model)
        {
            var user = _appUserService.Get(x => x.StaffId == model.StaffId).FirstOrDefault();                  
            await _userManager.RemoveFromRolesAsync(user,await _userManager.GetRolesAsync(user));
            foreach (var item in model.Roles)
            {
                var role = _appRoleService.Get(x => x.Id == item).FirstOrDefault().Name;
                var isInRole = await _userManager.IsInRoleAsync(user, role);
                if (!isInRole)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            foreach (var item in model.Permissions)
            {
                var role = _appRoleService.Get(x => x.Id == item).FirstOrDefault().Name;
                var isInRole = await _userManager.IsInRoleAsync(user, role);
                if (!isInRole)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            return RedirectToAction("Index", new { id = model.StaffId });
        }
    }

}