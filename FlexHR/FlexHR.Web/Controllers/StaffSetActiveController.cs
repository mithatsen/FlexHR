using FlexHR.Business.Interface;
using FlexHR.Entity.Concrete;
using FlexHR.Web.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class StaffSetActiveController : BaseIdentityController
    {
        private readonly IAppUserService _appUserService;
        public StaffSetActiveController(IAppUserService appUserService, UserManager<AppUser> userManager):base(userManager)
        {
            _appUserService = appUserService;
        }
        [Authorize(Roles = "ViewStaffSetActivePage,Manager")]

        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                var isExistPast = _appUserService.Get(x => x.StaffId == id && x.IsActive == false).FirstOrDefault();
                var isExistNow = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();

                ViewBag.StaffId = id;
                ViewBag.IsExist = isExistPast != null ? true : false;
                if (isExistNow != null)
                {
                    ViewBag.UserName = isExistNow.UserName;
                }

                var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();
                bool isUser = user != null ? true : false;
                ViewBag.IsUser = isUser;
                return View();
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }

        }
        [HttpPost]
        public bool DeleteUser(int id)
        {
            var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                _appUserService.Update(user);
            }
      
            return true;

        }
     
        public bool IsExistBefore(int id)
        {
            var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == false).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = true;
                _appUserService.Update(user);
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        public IActionResult GetUserInfo(int id)
        {
            var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();
            return Json(user.UserName);
        }

        [HttpGet]
        public IActionResult GetSwitchButtonPartial()
        {
           
            return PartialView("_SwitchButton");
        }

        
    }
}
