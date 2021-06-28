﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffShiftDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffShiftController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffShiftService _staffShiftService;
        public StaffShiftController(IStaffShiftService staffShiftService, IMapper mapper, IGeneralSubTypeService generalSubTypeService)
        {
            _staffShiftService = staffShiftService;
            _mapper = mapper;
            _generalSubTypeService = generalSubTypeService;
        }
        public IActionResult Index(int id)
        {
           
            ViewBag.StaffId = id;
            var staffShiftList = _staffShiftService.Get(p => p.StaffId == id && p.IsActive == true);
            var shiftModels = new List<ListStaffShiftDto>();
            foreach (var item in staffShiftList)
            {                        
                shiftModels.Add(_mapper.Map<ListStaffShiftDto>(item));
            }
            return View(shiftModels);
        }
        [HttpGet]
        public IActionResult GetUpdateStaffShiftModal(int id)
        {
            var staffShift = _staffShiftService.GetById(id);
           
          

            return PartialView("GetUpdateStaffShiftModal", _mapper.Map<ListStaffShiftDto>(staffShift));

        }
        [HttpPost]
        public IActionResult AddStaffShiftWithAjax(AddStaffShiftDto model)
        {
            model.IsActive = true;
            _staffShiftService.Add(_mapper.Map<StaffShift>(model));

            return Json(null);
        }
        [HttpPost]
        public IActionResult UpdateStaffShift(ListStaffShiftDto model)
        {
            model.IsActive = true;
            _staffShiftService.Update(_mapper.Map<StaffShift>(model));
            return RedirectToAction("Index",new {id=model.StaffId });

        }
    }
}
