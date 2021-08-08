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
        private readonly IPublicHolidayService _publicHolidayService;
        private readonly IEventService _eventService;
        private readonly IStaffShiftService _staffShiftService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IStaffLeaveService _staffLeaveService;
        private readonly IStaffPaymentService _staffPaymentService;
        public HomeController(IMapper mapper, IStaffShiftService staffShiftService, IStaffPersonelInfoService staffPersonelInfoService,IStaffLeaveService staffLeaveService,
            IStaffPaymentService staffPaymentService, IEventService eventService, IPublicHolidayService publicHolidayService)
        {
            _mapper = mapper;
            _publicHolidayService = publicHolidayService;
            _eventService = eventService;
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
            var birthDates = _mapper.Map<List<ListStaffBirthOnDashboardDto>>(_staffPersonelInfoService.Get(p => p.IsActive == true &&( p.BirthDate.Value.Month >= DateTime.Now.Month && p.BirthDate.Value.Day >= DateTime.Now.Day), null, "Staff").OrderBy(p=>p.BirthDate.Value.ToString("M")).ToList());
            var events = _mapper.Map<List<ListStaffEventOnDashboardDto>>(_eventService.Get(p => p.IsActive == true && p.Start > DateTime.Now).ToList());
            var publicDays = _mapper.Map<List<ListStaffPublicDayOnDashboardDto>>(_publicHolidayService.Get(p=>p.IsActive == true && p.Start > DateTime.Now).ToList());
            var upcomingLeaves = _mapper.Map<List<ListStaffUpcomingLeaveOnDashboardDto>>(_staffLeaveService.Get(p => p.GeneralStatusGeneralSubTypeId == 97 && p.IsActive == true && p.LeaveStartDate > DateTime.Now, null, "Staff,LeaveType").ToList());
            foreach (var item in shifts)
            {
                var temp = _staffPersonelInfoService.Get(x => x.StaffId == item.StaffId && x.IsActive == true).FirstOrDefault();
                var genderId = temp.GenderGeneralSubTypeId != null ? temp.GenderGeneralSubTypeId : 54;
                item.GenderGeneralSubTypeId = ((int)genderId != 52 && (int)genderId != 53) ? 54 : (int)genderId;
            }
            foreach (var item in leaves)
            {
                var temp = _staffPersonelInfoService.Get(x => x.StaffId == item.StaffId && x.IsActive == true).FirstOrDefault();
                var genderId = temp.GenderGeneralSubTypeId != null ? temp.GenderGeneralSubTypeId : 54;
                item.GenderGeneralSubTypeId = ((int)genderId != 52 && (int)genderId != 53) ? 54 : (int)genderId;
            }
            foreach (var item in payments)
            {
                var temp = _staffPersonelInfoService.Get(x => x.StaffId == item.StaffId && x.IsActive == true).FirstOrDefault();
                var genderId = temp.GenderGeneralSubTypeId != null ? temp.GenderGeneralSubTypeId : 54;
                item.GenderGeneralSubTypeId = ((int)genderId != 52 && (int)genderId != 53) ? 54 : (int)genderId;
            } 
            foreach (var item in birthDates)
            {
                var temp = _staffPersonelInfoService.Get(x => x.StaffId == item.StaffId && x.IsActive == true).FirstOrDefault();
                var genderId = temp.GenderGeneralSubTypeId != null ? temp.GenderGeneralSubTypeId : 54;
                item.GenderGeneralSubTypeId = ((int)genderId != 52 && (int)genderId != 53) ? 54 : (int)genderId;
            } 
            foreach (var item in upcomingLeaves)
            {
                var temp = _staffPersonelInfoService.Get(x => x.StaffId == item.StaffId && x.IsActive == true).FirstOrDefault();
                var genderId = temp.GenderGeneralSubTypeId!=null? temp.GenderGeneralSubTypeId:54;
                item.GenderGeneralSubTypeId = ((int)genderId != 52 && (int)genderId != 53) ? 54 : (int)genderId;
            }

            var model = new ListDashboardViewModel { StaffShifts = shifts, StaffLeaves=leaves,StaffPayments=payments ,BirtDates=birthDates,Events=events,PublicDays=publicDays,UpcomingLeaves=upcomingLeaves};
            return View(model);
        }
    }
}
