﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffNavbar: ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            return View(id);
        }
    }
}
