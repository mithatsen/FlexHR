﻿using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.AuthDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AuthController(SignInManager<AppUser> signInManager, IAppUserService appUserService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _appUserService = appUserService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            var user = _appUserService.Get(x => x.UserName == model.UserName && x.IsActive == true).FirstOrDefault();
            if (ModelState.IsValid && user != null)
            {
                var result=  await _userManager.GetRolesAsync(user);
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    ViewBag.LoginMessage = "true";
                    if (result.Contains("ViewAdminDashboard") || result.Contains("Manager"))
                    {                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "StaffGeneral" ,new { id=user.StaffId});
                    }                    
                }
            }
            ViewBag.LoginMessage = "false";
            return View("Index", model);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Auth");
        }
        public async Task<bool> ResetPassword(int id, string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, code, password);
                return true;
            }
            return false;
        }

        public async Task<IActionResult> UpdateUserName(string userName, int id)
        {
            try
            {
                var appUser = await _userManager.FindByNameAsync(userName);
                if (appUser != null)
                {
                    return Json("false");
                }
                else if (!String.IsNullOrWhiteSpace(userName) && userName.Length >= 3)
                {
                    var user = _appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault();
                    user.UserName = userName;
                    user.NormalizedUserName = userName.ToUpper();
                    _appUserService.Update(user);
                    return Json("true");
                }
                else
                {
                    return Json("not_valid");
                }

            }
            catch (Exception)
            {

                return Json("false");
            }

        }
        [HttpPost]

        public async Task<IActionResult> Register(string userName, string password, int id)
        {

            if (await _userManager.FindByNameAsync(userName) != null)
            {
                return Json("false");
            }
            AppUser user = new AppUser()
            {
                UserName = userName,
                StaffId = id,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return Json("true");
            }
            return Json("false");
        }
        [Authorize]
        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa Bulunamadı";
            }
            return View();
        }
    }
}
