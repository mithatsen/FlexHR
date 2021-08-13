﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
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
        public IActionResult Index()
        {
            ViewBag.Roles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125), "Id", "Description");
            ViewBag.PageRoles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 126), "Id", "Description");
            ViewBag.UserList = new SelectList(_appUserService.GetAll(), "Id", "UserName");

            return View();
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