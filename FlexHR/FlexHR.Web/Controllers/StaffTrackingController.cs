using FlexHR.Business.Interface;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FlexHR.Web.StringInfo.RoleInfo;

namespace FlexHR.Web.Controllers
{
    public class StaffTrackingController : Controller
    {
        private readonly IPublicHolidayService _publicHolidayService;
        private readonly IStaffService _staffService;
        private readonly IColorCodeService _colorCodeService;
        public StaffTrackingController(IStaffService staffService, IPublicHolidayService publicHolidayService, IColorCodeService colorCodeService)
        {
            _staffService = staffService;
            _publicHolidayService = publicHolidayService;
            _colorCodeService = colorCodeService;
        }
        [Authorize(Roles = "ViewStaffTrackingPage,Manager")]
        public IActionResult Index(DateTime dateTime)
        {
            TempData["Active"] = TempdataInfo.StaffTracking;
            DateTime date = dateTime.Year != 0001 ? dateTime : DateTime.Now;
            ViewBag.SortDate = date ;
            var result = _staffService.Get(x => x.IsActive == true);
            var staffTracking = _staffService.GetStaffTimeKeepingMonthly(date, result.ToList());
            var publicDays = _publicHolidayService.Get(x=>x.Start.Month== date.Month && x.IsActive==true).ToList();
            var colorCodes = _colorCodeService.GetAll();
            
            StaffTrackingMonthlyViewModal model = new StaffTrackingMonthlyViewModal { filterDate = date, ListStaffTimeKeepings = staffTracking ,PublicHolidays=publicDays,ColorCodes=colorCodes};

            return View(model);
        }
    }
}
