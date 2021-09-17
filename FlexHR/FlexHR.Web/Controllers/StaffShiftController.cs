using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.Entity.Concrete;
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
    public class StaffShiftController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffShiftService _staffShiftService;
        private readonly IStaffService _staffService;
        private readonly IAppUserService _appUserService;
        public StaffShiftController(IStaffShiftService staffShiftService, IStaffService staffService, IMapper mapper, IGeneralSubTypeService generalSubTypeService,
            UserManager<AppUser> userManager, IAppUserService appUserService) : base(userManager)
        {
            _staffShiftService = staffShiftService;
            _staffService = staffService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
            _appUserService = appUserService;
        }
        [Authorize(Roles = "ViewStaffShiftPage,Manager,Staff")]
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthority(id))
            {
                ViewBag.StaffId = id;
                var staffShiftList = _staffShiftService.Get(p => p.StaffId == id && p.IsActive == true);
                var shiftModels = new List<ListStaffShiftDto>();
                foreach (var item in staffShiftList)
                {
                    shiftModels.Add(_mapper.Map<ListStaffShiftDto>(item));
                }
                ViewBag.StaffShiftUpdateStatus = TempData["StaffShiftUpdateStatus"];
                return View(shiftModels);
            }
            else
            {
                return RedirectToAction("StatusCode", "Auth", new { code = 404 });
            }

        }
        [HttpGet]
        public IActionResult GetUpdateStaffShiftModal(int id)
        {
            var staffShift = _staffShiftService.GetById(id);
            return PartialView("GetUpdateStaffShiftModal", _mapper.Map<ListStaffShiftDto>(staffShift));

        }
        [HttpPost]
        public IActionResult AddStaffShiftWithAjax(AddStaffShiftDto formData)
        {
            if (ModelState.IsValid)
            {
                if (formData.IsCheckedApprove == true)
                {
                    formData.GeneralStatusGeneralSubTypeId = 97;
                    formData.WhoApprovedStaffId = _appUserService.Get(x=>x.UserName==User.Identity.Name).FirstOrDefault().StaffId;
                }
                formData.IsActive = true;
                _staffShiftService.Add(_mapper.Map<StaffShift>(formData));

                return Json("true");

            }
            return Json("false");
        }
        [HttpPost]
        public IActionResult AddStaffShiftMultipleWithAjax(AddStaffShiftMultipleDto formData)
        {
            if (ModelState.IsValid)
            {
                if (formData.IsCheckedApprove == true)
                {
                    formData.GeneralStatusGeneralSubTypeId = 97;
                    formData.WhoApprovedStaffId = _appUserService.Get(x => x.UserName == User.Identity.Name).FirstOrDefault().StaffId;
                }
                foreach (var item in formData.StaffId)
                {
                    AddStaffShiftDto temp = new AddStaffShiftDto
                    {
                        StaffId = item,
                        WhoApprovedStaffId = formData.WhoApprovedStaffId,
                        Description = formData.Description,
                        GeneralStatusGeneralSubTypeId = formData.GeneralStatusGeneralSubTypeId,
                        IsActive = true,
                        IsCheckedApprove = formData.IsCheckedApprove,
                        IsMailSentToStaff = formData.IsMailSentToStaff,
                        IsSentForApproval = formData.IsSentForApproval,
                        ShiftHour = formData.ShiftHour,
                        ShiftMinute = formData.ShiftMinute,
                        StartDate = formData.StartDate
                    };
                    _staffShiftService.Add(_mapper.Map<StaffShift>(temp));
                }
                return Json("true");

            }
            return Json("false");
        }

        [HttpPost]
        public IActionResult UpdateStaffShift(ListStaffShiftDto model)
        {
            model.IsActive = true;
            _staffShiftService.Update(_mapper.Map<StaffShift>(model));
            TempData["StaffShiftUpdateStatus"] = "true";
            return RedirectToAction("Index", new { id = model.StaffId });

        }
        [HttpPost]
        public bool DeleteStaffShift(int id)
        {
            try
            {
                var model = _staffShiftService.GetById(id);
                model.IsActive = false;
                _staffShiftService.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet]
        public IActionResult GetShiftRequestModal()
        {
            ViewBag.Staffs = new SelectList(_staffService.GetAll(), "StaffId", "NameSurname");
            int userId = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            ViewBag.StaffId = _appUserService.Get(x => x.Id == userId).FirstOrDefault().StaffId;
            return PartialView("_GetShiftRequestModal");
        }
    }
}
