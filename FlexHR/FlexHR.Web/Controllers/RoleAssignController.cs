﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class RoleAssignController : Controller
    {
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly RoleManager<AppRole> _roleManager;
        public RoleAssignController(IAppRoleService appRoleService, IStaffService staffService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IAppUserService appUserService)
        {
            _appRoleService = appRoleService;
            _staffService = staffService;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _appUserService = appUserService;
        }
        [Authorize(Roles = "ViewRoleAssignPage,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.AssignRole;
            ViewBag.Roles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125), "Id", "Description");
            ViewBag.PageRoles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 126), "Id", "Description");

            var userList = _appUserService.Get(x => x.IsActive == true);
            List<GetUserWithStaffName> users = new List<GetUserWithStaffName>();
            foreach (var item in userList)
            {
                users.Add(new GetUserWithStaffName { Id = item.Id, Name = _staffService.Get(x => x.StaffId == item.StaffId).FirstOrDefault().NameSurname });
            }
            ViewBag.UserList = new SelectList(users, "Id", "Name");

            return View();
        }
        public IActionResult GetRoleListByUserId(int id)
        {
            ViewBag.Id = id;
            var roles = _appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125 && x.IsActive == true).ToList();
            var permissions = _appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 126 && x.IsActive == true).ToList();
            var user = _appUserService.Get(x => x.Id == id && x.IsActive == true).FirstOrDefault();

            if (user != null)
            {
                var userAllRoles = _appUserService.GetAppRolesByUserId(id);
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
                var model = new ListRolesWithSelectedStaffRole { Permissions = permissions, Roles = roles, UserRoles = userRoles, UserPermissions = userPermissions };
                return PartialView("_GetRoleListByUserId", model);
            }
            else
            {
                var model = new ListRolesWithSelectedStaffRole { Permissions = permissions, Roles = roles, UserPermissions = new List<AppRole>(), UserRoles = new List<AppRole>() };
                return PartialView("_GetRoleListByUserId", model);
            }

        }
        [HttpPost]
        public async Task<bool> DeleteRole(int userId, int roleId)
        {
            var user = _appUserService.Get(x => x.Id == userId && x.IsActive == true).FirstOrDefault();
            var role = _appRoleService.Get(x => x.Id == roleId && x.IsActive == true).FirstOrDefault().Name;
            try
            {
                await _userManager.RemoveFromRoleAsync(user, role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }



        }
        public async Task<IActionResult> AddRole(AddMultipleRoleToStaffDto model)
        {
            var user = _appUserService.Get(x => x.Id == model.StaffId).FirstOrDefault();
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (model.Roles != null)
            {
                foreach (var item in model.Roles)
                {
                    var role = _appRoleService.Get(x => x.Id == item).FirstOrDefault().Name;
                    await _userManager.AddToRoleAsync(user, role);

                }
            }
            if (model.Permissions != null)
            {
                foreach (var item in model.Permissions)
                {
                    var role = _appRoleService.Get(x => x.Id == item).FirstOrDefault().Name;
                    await _userManager.AddToRoleAsync(user, role);

                }
            }

            return RedirectToAction("Index", new { id = model.StaffId });
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthorizeWithAjax(AddAuthorizeTypeList formData)
        {
            foreach (var item in formData.Users)
            {
                var user = _appUserService.GetById(item);
                if (formData.Roles != null)
                {
                    foreach (var roleType in formData.Roles)
                    {
                        var appRole = _appRoleService.GetById(roleType);
                        var isInRole = await _userManager.IsInRoleAsync(user, appRole.Name);
                        if (!isInRole)
                        {
                            await _userManager.AddToRoleAsync(user, appRole.Name);
                        }
                    }

                }
                if (formData.AuthorizeTypes != null)
                {
                    foreach (var authorizeRole in formData.AuthorizeTypes)
                    {
                        var authorizeType = _appRoleService.GetById(authorizeRole);
                        var isInRole = await _userManager.IsInRoleAsync(user, authorizeType.Name);
                        if (!isInRole)
                        {
                            await _userManager.AddToRoleAsync(user, authorizeType.Name);
                        }
                    }
                }
            }

            return Json("true");
        }
    }
}
