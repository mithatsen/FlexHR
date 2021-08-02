using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.GeneralSubTypeDtos;
using FlexHR.DTO.Dtos.LeaveTypeDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class SystemVariableController : Controller
    {
        private readonly IGeneralTypeService _generalTypeService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public SystemVariableController(IGeneralTypeService generalTypeService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, ILeaveTypeService leaveTypeService)
        {
            _generalTypeService = generalTypeService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
        }

        public IActionResult Index()
        {
            ViewBag.GeneralTypeList = new SelectList(_generalTypeService.Get(p => p.IsActive == true), "GeneralTypeId", "Description");
            ViewBag.GeneralSubTypeUpdateStatus = TempData["GeneralSubTypeUpdateStatus"] != null ? TempData["GeneralSubTypeUpdateStatus"] : "false";
            return View();
        }
        public IActionResult GetGeneralTypeById(int id)
        {
            var result = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId(id);
            return PartialView("_GetGeneralSubTypeTable", _mapper.Map<List<ListGeneralSubTypeDto>>(result));
        } 
       
        public IActionResult GetUpdateGeneralSubTypeModal(int id)
        {
            var result = _generalSubTypeService.GetById(id);
            return PartialView("_GetGeneralSubTypeUpdateModal", _mapper.Map<ListGeneralSubTypeDto>(result));
        }

        [HttpPost]
        public bool AddGeneralSubType(AddGeneralSubTypeDto model)
        {
            var temp = _generalSubTypeService.Get(p => p.GeneralTypeId == model.GeneralTypeId).LastOrDefault().Name;
            var temp2 = Convert.ToInt32(temp.Substring(1)) + 1;
            temp = temp.Substring(0, 1);
            temp += temp2;
            model.Name = temp;
            _generalSubTypeService.Add(_mapper.Map<GeneralSubType>(model));
            //TempData["GeneralSubTypeUpdateStatus"] = "true";
            return true;
        }
        [HttpPost]
        public IActionResult UpdateGeneralSubType(ListGeneralSubTypeDto model)
        {
            _generalSubTypeService.Update(_mapper.Map<GeneralSubType>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public bool DeleteGeneralSubtype(int id)
        {
            try
            {
                var career = _generalSubTypeService.GetById(id);
                career.IsActive = false;
                _generalSubTypeService.Update(career);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        public IActionResult GetLeaveTypeList()
        {
            var result = _leaveTypeService.Get(x=>x.IsActive==true);
            return PartialView("_GetLeaveTypeTable", _mapper.Map<List<ListLeaveTypeDto>>(result));
        }
    }
}
