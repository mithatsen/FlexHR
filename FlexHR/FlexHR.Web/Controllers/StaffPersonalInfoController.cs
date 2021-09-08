using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPersonalInfoDtos;
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
    public class StaffPersonalInfoController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        public StaffPersonalInfoController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, UserManager<AppUser> userManager, IStaffPersonelInfoService staffPersonelInfoService) : base(userManager)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffPersonelInfoService = staffPersonelInfoService;
        }
        [Authorize(Roles = "ViewStaffPersonalInfoPage,Manager")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                var personelInfo = _staffPersonelInfoService.GetPersonelInfoByStaffId(id);
                ViewBag.MaritalStatusList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.MaritalStatus), "GeneralSubTypeId", "Description");
                ViewBag.DegreeOfDisabilityList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.DegreeOfDisability), "GeneralSubTypeId", "Description");
                ViewBag.EducationStatusList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationStatus), "GeneralSubTypeId", "Description");
                ViewBag.GenderList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Gender), "GeneralSubTypeId", "Description");
                ViewBag.BloodGroupList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.BloodGroup), "GeneralSubTypeId", "Description");
                ViewBag.EducationLevelList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationLevel), "GeneralSubTypeId", "Description");

                ViewBag.StaffPersonalInfoUpdateStatus = TempData["StaffPersonalInfoUpdateStatus"];
                return View(_mapper.Map<UpdateStaffPersonalInfoDto>(personelInfo));
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }
        }
        [HttpPost]
        public IActionResult UpdateStaffPersonalInfo(UpdateStaffPersonalInfoDto model)
        {
            if (ModelState.IsValid)
            {

                model.MaritalStatusGeneralSubTypeId = model.MaritalStatusGeneralSubTypeId == -1 ? null : model.MaritalStatusGeneralSubTypeId;
                model.DegreeOfDisabilityGeneralSubTypeId = model.DegreeOfDisabilityGeneralSubTypeId == -1 ? null : model.DegreeOfDisabilityGeneralSubTypeId;
                model.BloodGroupGeneralSubTypeId = model.BloodGroupGeneralSubTypeId == -1 ? null : model.BloodGroupGeneralSubTypeId;
                model.EducationStatusGeneralSubTypeId = model.EducationStatusGeneralSubTypeId == -1 ? null : model.EducationStatusGeneralSubTypeId;
                model.GenderGeneralSubTypeId = model.GenderGeneralSubTypeId == -1 ? null : model.GenderGeneralSubTypeId;
                _staffPersonelInfoService.Update(_mapper.Map<StaffPersonelInfo>(model));
                TempData["StaffPersonalInfoUpdateStatus"] = "true";
                return RedirectToAction("Index", new { id = model.StaffId });
            }
            return RedirectToAction("Index", new { id = model.StaffId });
        }
    }
}
