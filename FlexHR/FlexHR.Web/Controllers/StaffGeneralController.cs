﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.AppUserDtos;
using FlexHR.DTO.Dtos.StaffGeneralDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using FlexHR.Web.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    [Authorize]
    public class StaffGeneralController : BaseIdentityController
    {
        private readonly IStaffService _staffService;
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        public StaffGeneralController(IStaffService staffService, IAppUserService appUserService, IAppRoleService appRoleService, IGeneralSubTypeService generalSubTypeService, IMapper mapper,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager):base(userManager)
        {
            _staffService = staffService;
            _appRoleService = appRoleService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _appUserService = appUserService;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "ViewStaffGeneralPage,Manager,Staff")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                var staff = _staffService.GetById(id);
                ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
                ViewBag.StaffGeneralUpdateStatus = TempData["StaffGeneralUpdateStatus"];
                return View(_mapper.Map<ListStaffGeneralDto>(staff));
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });  
            }
           
        }
        [HttpPost]
        public IActionResult UpdateStaffGeneral(UpdateStaffGeneralDto model)
        {
            _staffService.Update(_mapper.Map<Staff>(model));
            TempData["StaffGeneralUpdateStatus"] = "true";

            return RedirectToAction("Index", new { id = model.StaffId });

        }
    }
}
