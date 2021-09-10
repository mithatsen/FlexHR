using FlexHR.Business.Interface;
using FlexHR.DTO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffTrackingController : Controller
    {

        private readonly IStaffService _staffService;
        public StaffTrackingController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        public IActionResult Index(DateTime dateTime)
        {

            DateTime date = dateTime.Year != 0001 ? dateTime : DateTime.Now;
            ViewBag.SortDate = date ;
            var result = _staffService.Get(x => x.IsActive == true);
            var staffTracking = _staffService.GetStaffTimeKeepingMonthly(date, result.ToList());
          
            StaffTrackingMonthlyViewModal model = new StaffTrackingMonthlyViewModal { filterDate = date, ListStaffTimeKeepings = staffTracking };

            return View(model);
        }
    }
}
