using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize(Roles = "ViewShiftRequestPage,Manager")]
        public IActionResult Index()
        {
            var approvedShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.IsActive==true,null,"Staff").ToList());
            var pendingApprovalShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true,null, "Staff").ToList());
            var rejectedShifts = _mapper.Map<List<ListStaffShiftDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 98 && p.IsActive == true,null, "Staff").ToList());
         
            ListShiftViewModel listShiftViewModel = new ListShiftViewModel
                {              
                   ApprovedShift=approvedShifts,
                   PendingApprovalLeaves=pendingApprovalShifts,
                   RejectedShift=rejectedShifts                 
                };
            return View(listShiftViewModel);           
        }
        [HttpPost]
        public bool ApproveShift(int id)
        {
            var temp=_staffShiftService.GetById(id);
            temp.GeneralStatusGeneralSubTypeId = 97;
            try
            {
                _staffShiftService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        [HttpPost]
        public bool RejectShift(int id)
        {
            var temp = _staffShiftService.GetById(id);
            temp.GeneralStatusGeneralSubTypeId = 98;
            try
            {
                _staffShiftService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}
