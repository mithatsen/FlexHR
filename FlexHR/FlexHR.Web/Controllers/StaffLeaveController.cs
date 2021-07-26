using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffLeaveController : Controller
    {
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffService _staffService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        private readonly ILeaveRuleService _leaveRuleService;
        public StaffLeaveController(IStaffLeaveService staffLeaveService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, ILeaveTypeService leaveTypeService, ILeaveRuleService leaveRuleService,IStaffService staffService)
        {
            _staffLeaveService = staffLeaveService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
            _leaveRuleService = leaveRuleService;
            _staffService = staffService;
        }
        public IActionResult Index(int id)
        {
      


            ViewBag.StaffId = id;

            var staffLeaveList = _staffLeaveService.Get(p => p.StaffId == id && p.IsActive == true);
            var leaveModels = new List<ListStaffLeaveDto>();
            var staff = _staffService.GetById(id);

            var totalLeaveDeserved=CalculateTotalLeaveAmountDeservedBySeniority(staff.JobJoinDate);
            foreach (var item in staffLeaveList)
            {
                var leaveModel = _mapper.Map<ListStaffLeaveDto>(item);
                leaveModel.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
                leaveModel.StatusType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.GeneralStatusGeneralSubTypeId);
                leaveModels.Add(leaveModel);
            };
            ViewBag.StaffLeaveUpdateStatus = TempData["StaffLeaveUpdateStatus"];
            ViewBag.LeaveTypes= _leaveTypeService.GetAll();
            return View(leaveModels);
        }
        [HttpGet]
        public IActionResult GetUpdateStaffLeaveModal(int id)
        {
            var result = _staffLeaveService.GetById(id);
            ViewBag.LeaveTypes = _leaveTypeService.GetAll();
            var leaveModel = new ListStaffLeaveDto
            {
                Description = result.Description,
                LeaveStartDate = result.LeaveStartDate,
                LeaveEndDate = result.LeaveEndDate,
                LeaveTypeId = result.LeaveTypeId,
                TotalDay = result.TotalDay,
                StaffId = result.StaffId,
                StaffLeaveId = result.StaffLeaveId,
                GeneralStatusGeneralSubTypeId = result.GeneralStatusGeneralSubTypeId,
                IsActive = result.IsActive,
                LeaveType= _leaveTypeService.GetById(result.LeaveTypeId).Name

            };
            return PartialView("GetUpdateStaffLeaveModal", leaveModel);
        }

        [HttpPost]
        public IActionResult AddStaffLeaveWithAjax(AddStaffLeaveDto model)
        {

            _staffLeaveService.Add(_mapper.Map<StaffLeave>(model));

            return Json("true");
        }
        [HttpPost]
        public IActionResult UpdateStaffLeave(ListStaffLeaveDto model)
        {

            _staffLeaveService.Update(_mapper.Map<StaffLeave>(model));
            TempData["StaffLeaveUpdateStatus"] = "true";
            return RedirectToAction("Index", new { id = model.StaffId });

        }
        [HttpPost]
        public bool DeleteStaffLeave(int id)
        {
           

            try
            {
                var leave = _staffLeaveService.GetById(id);
                leave.IsActive = false;
                _staffLeaveService.Update(leave);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  int CalculateTotalLeaveAmountDeservedBySeniority(DateTime startDate)
        {
            var models = _leaveRuleService.GetAll().OrderBy(p=>p.SeniorityYear);
            var seniorityLevel = (DateTime.Now-startDate).Days/365;
            int oldCount = 0;
            int leaveAmount = 14;
            var totalDayDeserved = 0;
            foreach (var item in models)
            {
                if (seniorityLevel > item.SeniorityYear)
                {
                    totalDayDeserved+=(item.SeniorityYear - oldCount) * leaveAmount;
                }
                else
                {
                    totalDayDeserved += (seniorityLevel - oldCount) * leaveAmount;
                    leaveAmount += item.AditionalLeaveAmount;
                    oldCount = item.SeniorityYear;
                    break;
                }
                leaveAmount += item.AditionalLeaveAmount;
                oldCount = item.SeniorityYear;
              
            }
            if (seniorityLevel - oldCount > 0)
            {
                totalDayDeserved += (seniorityLevel - oldCount) * leaveAmount;
            }


            return totalDayDeserved;
        }

    }
}
