﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffCareer : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}