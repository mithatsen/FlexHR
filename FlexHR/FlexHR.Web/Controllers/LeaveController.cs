using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffLeaveDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{

    public class LeaveController : Controller
    {
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public LeaveController(IStaffLeaveService staffLeaveService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, ILeaveTypeService leaveTypeService)
        {
            _staffLeaveService = staffLeaveService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
        }
        public IActionResult Index()
        
        {
            var approvedLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 97).ToList());
            var pendingApprovalLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 96).ToList());
            var rejectedLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 98).ToList());
            var upcomingLeaves = _mapper.Map<List<ListStaffLeaveDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.LeaveStartDate > DateTime.Now).ToList());

            foreach (var item in approvedLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in pendingApprovalLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in rejectedLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }
            foreach (var item in upcomingLeaves)
            {
                item.LeaveType = _leaveTypeService.GetById(item.LeaveTypeId).Name;
            }

            ListLeaveViewModel listLeaveViewModel = new ListLeaveViewModel
            {
                ApprovedLeaves =approvedLeaves,
                PendingApprovalLeaves = pendingApprovalLeaves,
                RejectedLeaves = rejectedLeaves,
                UpcomingLeaves = upcomingLeaves
            };
            return View(listLeaveViewModel);
        }

        public IActionResult Reports()
        {
            return View();
        }
    }
}
