using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.AppUserDtos;
using FlexHR.DTO.Dtos.StaffGeneralDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffGeneralController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IAppRoleService _appRoleService;
        private readonly IAppUserService _appUserService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public StaffGeneralController(IStaffService staffService, IAppUserService appUserService, IAppRoleService appRoleService, IGeneralSubTypeService generalSubTypeService, IMapper mapper,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _staffService = staffService;
            _appRoleService = appRoleService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _appUserService = appUserService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            int roleId = -1;
            var staff = _staffService.GetById(id);
            var user = _appUserService.Get(x => x.StaffId == id).FirstOrDefault();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                if (role.AuthorizeTypeGeneralSubTypeId == 125)
                {
                    roleId = role.Id;
                }
            }
            ViewBag.Roles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125), "Id", "Description");
            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
            var appUserList = _mapper.Map<ListAppUserDto>(_appUserService.Get(x => x.StaffId == id && x.IsActive == true).FirstOrDefault());
            ListStaffGeneralDto listStaffGeneralDto = new ListStaffGeneralDto
            {
                ContractTypeGeneralSubTypeId = staff.ContractTypeGeneralSubTypeId,
                EmailJob = staff.EmailJob,
                StaffId = staff.StaffId,
                EmailPersonal = staff.EmailPersonal,
                JobJoinDate = staff.JobJoinDate,
                NameSurname = staff.NameSurname,
                PhoneJob = staff.PhoneJob,
                PhonePersonal = staff.PhonePersonal,
                JobFinishDate = staff.JobFinishDate,
                AppUser = appUserList,
                WillUseSystem = staff.WillUseSystem,
                RoleId = roleId

            };
            ViewBag.StaffGeneralUpdateStatus = TempData["StaffGeneralUpdateStatus"];
            return View(listStaffGeneralDto);
        }
        [HttpPost]
        public IActionResult UpdateStaffGeneral(UpdateStaffGeneralDto model)
        {

            //   var staffRole = _staffRoleService.GetUserRoleByStaffId(model.StaffId);             
            //staffRole.RoleId = model.RoleId;
            //_staffRoleService.Update(staffRole);
            // var abc = _appUserService.Get(x => x.StaffId == model.StaffId, null, "Staff").FirstOrDefault();
            //var role = _appRoleService.Get(x => x.Id == model.RoleId).FirstOrDefault().Name;
            // await _userManager.AddToRoleAsync(abc, role);
            _staffService.Update(_mapper.Map<Staff>(model));
            TempData["StaffGeneralUpdateStatus"] = "true";

            return RedirectToAction("Index", new { id = model.StaffId });

        }
    }
}
