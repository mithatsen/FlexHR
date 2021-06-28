using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffFileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        public StaffFileController(IMapper mapper, IGeneralSubTypeService generalSubTypeService)
        {
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
        }
        public IActionResult Index(int id)
        {
            id = 2;
            ViewBag.StaffId = id;
            ViewBag.FileTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.FileType), "GeneralSubTypeId", "Description");
            return View();
        }
    }
}
