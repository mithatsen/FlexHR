using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffCareerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult UpdateStaffCareer(int id)
        {
          
            return View();
        }
    }
}
