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
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public StaffLeaveController(IStaffLeaveService staffLeaveService, IGeneralSubTypeService generalSubTypeService, IMapper mapper)
        {
            _staffLeaveService = staffLeaveService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
        }
        public IActionResult Index(int id)
        {
        
            ViewBag.LeaveTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.LeaveType), "GeneralSubTypeId", "Description");
            ViewBag.StaffId = id; 

            var staffLeaveList = _staffLeaveService.Get(p => p.StaffId == id && p.IsActive == true);
            var leaveModels = new List<ListStaffLeaveDto>();
            foreach (var item in staffLeaveList)
            {
                var leaveModel = _mapper.Map<ListStaffLeaveDto>(item);
                leaveModel.LeaveType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.LeaveTypeGeneralSubTypeId);
                leaveModel.StatusType = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.GeneralStatusGeneralSubTypeId);
                leaveModels.Add(leaveModel);
            };
           
            return View(leaveModels);
    }
    [HttpGet]
    public IActionResult GetUpdateStaffLeaveModal(int id)
    {
        var result = _staffLeaveService.GetById(id);
        ViewBag.LeaveTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.LeaveType), "GeneralSubTypeId", "Description");
        var leaveModel = new ListStaffLeaveDto
        {
            Description = result.Description,
            LeaveStartDate = result.LeaveStartDate,
            LeaveEndDate = result.LeaveEndDate,
            LeaveTypeGeneralSubTypeId = result.LeaveTypeGeneralSubTypeId,
            TotalDay = result.TotalDay,
            StaffId = result.StaffId,
            StaffLeaveId = result.StaffLeaveId,
            GeneralStatusGeneralSubTypeId = result.GeneralStatusGeneralSubTypeId,
            IsActive = result.IsActive

        };

        return PartialView("GetUpdateStaffLeaveModal", leaveModel);

    }

        [HttpPost]
        public IActionResult AddStaffLeaveWithAjax(AddStaffLeaveDto model)
        {

            _staffLeaveService.Add(_mapper.Map<StaffLeave>(model));

            return Json(null);
        }
        [HttpPost]
        public IActionResult UpdateStaffLeave(ListStaffLeaveDto model)
        {

            _staffLeaveService.Update(_mapper.Map<StaffLeave>(model));
            return RedirectToAction("Index",new { id = model.StaffId });

        }
    }
}
