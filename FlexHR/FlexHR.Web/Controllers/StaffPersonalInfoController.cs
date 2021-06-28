using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffPersonalInfoDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffPersonalInfoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        public StaffPersonalInfoController(IMapper mapper, IGeneralSubTypeService generalSubTypeService, IStaffPersonelInfoService staffPersonelInfoService)
        {
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _staffPersonelInfoService = staffPersonelInfoService;
        }
        public IActionResult Index(int id)
        {
           
            var personelInfo = _staffPersonelInfoService.GetPersonelInfoByStaffId(id);
            ViewBag.MaritalStatusList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.MaritalStatus), "GeneralSubTypeId", "Description");
            ViewBag.DegreeOfDisabilityList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.DegreeOfDisability), "GeneralSubTypeId", "Description");
            ViewBag.EducationStatusList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationStatus), "GeneralSubTypeId", "Description");
            ViewBag.GenderList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Gender), "GeneralSubTypeId", "Description");
            ViewBag.BloodGroupList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.BloodGroup), "GeneralSubTypeId", "Description");
            ViewBag.EducationLevelList = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationLevel), "GeneralSubTypeId", "Description");

            return View(_mapper.Map<UpdateStaffPersonalInfoDto>(personelInfo));
        }
        [HttpPost]
        public IActionResult UpdateStaffPersonalInfo(UpdateStaffPersonalInfoDto model)
        {
            if (ModelState.IsValid)
            {

                _staffPersonelInfoService.Update(_mapper.Map<StaffPersonelInfo>(model));
                return RedirectToAction("Index",new {id= model.StaffId });
            }
            return View();
        }
    }
}
