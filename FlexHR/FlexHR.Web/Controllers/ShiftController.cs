using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStaffShiftService _staffShiftService;
        public ShiftController(IMapper mapper, IStaffShiftService staffShiftService)
        {
            _mapper = mapper;
            _staffShiftService = staffShiftService;
        }
        public IActionResult Index()
        {
            var approvedShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.IsActive==true).ToList());
            var pendingApprovalShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true).ToList());
            var rejectedShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 98 && p.IsActive == true).ToList());

            ListShiftViewModel listShiftViewModel = new ListShiftViewModel
                {              
                   ApprovedShift=approvedShifts,
                   PendingApprovalLeaves=pendingApprovalShifts,
                   RejectedShift=rejectedShifts                 
                };
            return View(listShiftViewModel);           
        }
    }
}
