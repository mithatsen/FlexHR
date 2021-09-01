using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
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
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        private readonly IMapper _mapper;
        //   private readonly IStaffRoleService _staffRoleService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IAppRoleService _appRoleService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IStaffOtherInfoService _staffOtherInfoService;
        private readonly IStaffFileService _staffFileService;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly IFileColumnService _fileColumnService;
        private readonly IStaffCareerService _staffCareerService;

        public StaffController(IStaffService staffService, IMapper mapper, IGeneralSubTypeService generalSubTypeService, IStaffPersonelInfoService staffPersonelInfoService,
                                      IAppRoleService appRoleService, IStaffOtherInfoService staffOtherInfoService, IStaffFileService staffFileService, UserManager<AppUser> userManager,
                                       RoleManager<AppRole> roleManager, IAppUserService appUserService, IStaffCareerService staffCareerService, IFileColumnService fileColumnService)
        {
            _staffService = staffService;
            _fileColumnService = fileColumnService;
            _mapper = mapper;
            _userManager = userManager;
            //_staffRoleService = staffRoleService;
            _generalSubTypeService = generalSubTypeService;
            _appRoleService = appRoleService;
            _staffPersonelInfoService = staffPersonelInfoService;
            _staffOtherInfoService = staffOtherInfoService;
            _staffFileService = staffFileService;
            _roleManager = roleManager;
            _appUserService = appUserService;
            _staffCareerService = staffCareerService;
        }
        [Authorize(Roles = "ViewPersonalsPage,Manager")]
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Staff;
            var result = _staffService.Get(x => x.IsActive == true, null, "StaffFile");

            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
            ViewBag.Roles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 125), "Id", "Description");
            ViewBag.PageRoles = new SelectList(_appRoleService.Get(x => x.AuthorizeTypeGeneralSubTypeId == 126), "Id", "Description");


            var models = _mapper.Map<List<ListStaffDto>>(result);
            foreach (var item in models)
            {
                //***************************************************************************
                var careerResult = _staffCareerService.Get(x => x.IsActive == true && x.StaffId == item.StaffId, null, "CompanyBranch").OrderByDescending(p => p.JobStartDate).ToList();

                if (careerResult.Count > 0)
                {
                    var activeCareer = careerResult.First();

                    item.DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.DepartmantGeneralSubTypeId);
                    item.BranchName = activeCareer.CompanyBranch != null ? activeCareer.CompanyBranch.BranchName : "-";
                    item.Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.TitleGeneralSubTypeId);
                }
                else
                {
                    item.DepartmantName = "-";
                    item.BranchName = "-";
                    item.Title = "-";
                }
                //***************************************************************************
                var picture = _staffFileService.Get(x => x.StaffId == item.StaffId && x.IsActive == true && x.FileGeneralSubTypeId == 3).OrderByDescending(x => x.StaffFileId).FirstOrDefault();

                item.PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : "-";
            }

            return View(models);


        }

        public IActionResult StaffSearchWithAjax(IFormCollection formData)
        {
            IEnumerable<Staff> result;
            if (formData == null || String.IsNullOrWhiteSpace(formData.Keys.FirstOrDefault()))
            {
                result = _staffService.GetAll();
            }
            else
            {
                result = _staffService.GetStaffBySearchString(formData.Keys.FirstOrDefault());
            }
            var models = _mapper.Map<List<ListStaffDto>>(result);
            foreach (var item in models)
            {
                var picture = _staffFileService.Get(x => x.StaffId == item.StaffId && x.IsActive == true && x.FileGeneralSubTypeId == 3).OrderByDescending(x => x.StaffFileId).FirstOrDefault();

                item.PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : null;
            }
            return PartialView("_GetStaffCardBySearch", models);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = _appUserService.Get(x => x.StaffId == id).FirstOrDefault();
            user.IsActive = false;
            _appUserService.Update(user);
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

            return Json("true");

        }
        [HttpPost]
        public async Task<IActionResult> AddStaffWithAjax(AddStaffDto modal)
        {
            var staffResult = new Staff();
            int staffId;

            if (modal.WillUseSystem != false)
            {
                var appUser = await _userManager.FindByNameAsync(modal.UserName);
                if (appUser != null)
                {
                    return Json("false");
                }
                else if (!String.IsNullOrWhiteSpace(modal.UserName) && modal.UserName.Length >= 3)
                {
                    staffResult = _staffService.AddResult(_mapper.Map<Staff>(modal));
                    staffId = staffResult.StaffId;         
                                           
                    AppUser user = new AppUser()
                    {
                        UserName = modal.UserName,
                        Email = modal.EmailPersonal,
                        StaffId = staffResult.StaffId,
                        IsActive = true
                    };

                    await _userManager.CreateAsync(user, modal.Password);
                    if (modal.RoleId > 0)
                    {
                        var role = _appRoleService.Get(x => x.Id == modal.RoleId).FirstOrDefault().Name;
                        var dene = await _userManager.AddToRoleAsync(user, role);
                    }

                    if (modal.PageRoles != null)
                    {
                        foreach (var item in modal.PageRoles)
                        {
                            var rolePage = _appRoleService.Get(x => x.Id == item).FirstOrDefault().Name;
                            var isInRole = await _userManager.IsInRoleAsync(user, rolePage);
                            if (!isInRole)
                            {
                                await _userManager.AddToRoleAsync(user, rolePage);
                            }
                            await _userManager.AddToRoleAsync(user, rolePage);
                        }
                    }

                    _staffOtherInfoService.Add(new StaffOtherInfo { StaffId = staffId, IsActive = true });
                    _staffPersonelInfoService.Add(new StaffPersonelInfo
                    {
                        StaffId = staffId,
                        MaritalStatusGeneralSubTypeId = null,
                        GenderGeneralSubTypeId = null,
                        DegreeOfDisabilityGeneralSubTypeId = null,
                        BloodGroupGeneralSubTypeId = null,
                        EducationLevelGeneralSubTypeId = null,
                        EducationStatusGeneralSubTypeId = null,
                        IsActive = true
                    });
                    return Json("true");
                }
                else
                {
                    return Json("not_valid");
                }
            }
            else
            {
                staffResult = _staffService.AddResult(_mapper.Map<Staff>(modal));
                staffId = staffResult.StaffId;
                _staffOtherInfoService.Add(new StaffOtherInfo { StaffId = staffId, IsActive = true });
                _staffPersonelInfoService.Add(new StaffPersonelInfo
                {
                    StaffId = staffId,
                    MaritalStatusGeneralSubTypeId = null,
                    GenderGeneralSubTypeId = null,
                    DegreeOfDisabilityGeneralSubTypeId = null,
                    BloodGroupGeneralSubTypeId = null,
                    EducationLevelGeneralSubTypeId = null,
                    EducationStatusGeneralSubTypeId = null,
                    IsActive = true
                });
                return Json("true");
            }
        }

        public IActionResult RemoveStaff(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff!=null)
            {
                staff.IsActive = false;
                _staffService.Update(staff);
                var user = _appUserService.Get(x => x.StaffId == id).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = false;
                    _appUserService.Update(user);
                } 
                return RedirectToAction("Index", "Home");
            }
            TempData["StaffRemoveStatus"] = "false";
            return RedirectToAction("Index", "Home");
        }
    }
}
