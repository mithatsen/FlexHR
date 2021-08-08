using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.DashboardDtos;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStaffShiftService _staffShiftService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffPaymentService _staffPaymentService;
        public HomeController(IMapper mapper, IStaffShiftService staffShiftService, IStaffPersonelInfoService staffPersonelInfoService,IStaffLeaveService staffLeaveService, IStaffPaymentService staffPaymentService)
        {
            _mapper = mapper;
            _staffShiftService = staffShiftService;
            _staffPersonelInfoService = staffPersonelInfoService;
            _staffLeaveService = staffLeaveService;
            _staffPaymentService = staffPaymentService;
        }
    
        public IActionResult Index()
        {
            var shifts = _mapper.Map<List<ListStaffShiftOnDashboardDto>>(_staffShiftService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true, null, "Staff")).ToList();
            var payments = _mapper.Map<List<ListStaffPaymentOnDashboardDto>>(_staffPaymentService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true, null, "Staff").ToList());
            var leaves = _mapper.Map<List<ListStaffLeaveOnDashboardDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 96 && p.IsActive == true, null, "Staff,LeaveType").ToList());
            foreach (var item in shifts)
            {
                var genderId= (int)_staffPersonelInfoService.Get(x => x.StaffId == item.StaffId).FirstOrDefault().GenderGeneralSubTypeId;
                item.GenderGeneralSubTypeId = (genderId != 52 && genderId != 53) ? 54 : genderId;           
            }
            foreach (var item in leaves)
            {
                var genderId = (int)_staffPersonelInfoService.Get(x => x.StaffId == item.StaffId).FirstOrDefault().GenderGeneralSubTypeId;
                item.GenderGeneralSubTypeId = (genderId != 52 && genderId != 53) ? 54 : genderId;
            }
            foreach (var item in payments)
            {
                var genderId = (int)_staffPersonelInfoService.Get(x => x.StaffId == item.StaffId).FirstOrDefault().GenderGeneralSubTypeId;
                item.GenderGeneralSubTypeId = (genderId != 52 && genderId != 53) ? 54 : genderId;
            }

            var model = new ListDashboardViewModel { StaffShifts = shifts, StaffLeaves=leaves,StaffPayments=payments };
            return View(model);
        }
    }
}
