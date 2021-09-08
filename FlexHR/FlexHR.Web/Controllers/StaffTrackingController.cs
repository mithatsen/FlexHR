using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffTrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
