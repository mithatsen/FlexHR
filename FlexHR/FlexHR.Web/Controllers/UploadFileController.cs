﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class UploadFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RefectoryList()
        {
            return View();
        }
    }
}