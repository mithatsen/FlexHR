using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCareerDtos;
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
    public class StaffCareerController : Controller
    {
        private readonly IStaffCareerService _staffCareerService;
        private readonly ICompanyService _companyService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IMapper _mapper;
        public StaffCareerController(IStaffCareerService staffCareerService, ICompanyService companyService, IGeneralSubTypeService generalSubTypeService, IMapper mapper)
        {
            _staffCareerService = staffCareerService;
            _companyService = companyService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;

        }
        public IActionResult Index(int id)
        {
            
            ViewBag.StaffId= id;
            var careerModels = new List<ListStaffCareerDto>();
            var careerResult = _staffCareerService.GetAllTableByStaffId(id);


            ViewBag.Companies = new SelectList(_companyService.GetAll(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");

            foreach (var item in careerResult)
            {
                var careerModel = new ListStaffCareerDto
                {
                    StaffCareerId = item.StaffCareerId,
                    JobStartDate = item.JobStartDate,
                    JobFinishDate = item.JobFinishDate,
                    CompanyName = _companyService.GetCompanyNameByCompanyId(item.CompanyId),
                    BranchName = item.CompanyBranch != null ? item.CompanyBranch.BranchName : "-",
                    IsActive = item.IsActive,
                    ModeOfOperation = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.ModeOfOperationGeneralSubTypeId),
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.DepartmantGeneralSubTypeId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.TitleGeneralSubTypeId),
                };
                careerModels.Add(careerModel);
            }
            return View(careerModels);
        }
        [HttpPost]
        public IActionResult AddStaffCareerWithAjax(AddStaffCareerDto model)
        {
            ViewBag.StaffId = model.StaffId;
            if (model.CompanyBranchId == -1)
            {
                model.CompanyBranchId = null;
            }
            if (model.JobFinishDate == null || model.JobFinishDate > DateTime.Now)
            {
                model.IsActive = true;
            }
            else
            {
                model.IsActive = false;
            }

            _staffCareerService.Add(_mapper.Map<StaffCareer>(model));


            return Json("");
        }
        [HttpGet]
        public IActionResult GetUpdateStaffCareerModal(int id)
        {
            var staffCareer = _staffCareerService.GetById(id);
            ViewBag.Companies = new SelectList(_companyService.GetAll(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");
            var x = _mapper.Map<ListStaffCareerDto>(staffCareer);
            return PartialView("GetUpdateStaffCareerModal", _mapper.Map<ListStaffCareerDto>(staffCareer));

        }
        [HttpPost]
        public IActionResult UpdateStaffCareer(ListStaffCareerDto model)
        {
            
            if (model.CompanyBranchId == -1)
            {
                model.CompanyBranchId = null;
            }
            if (model.JobFinishDate == null || model.JobFinishDate > DateTime.Now)
            {
                model.IsActive = true;
            }
            else
            {
                model.IsActive = false;
            }

            _staffCareerService.Update(_mapper.Map<StaffCareer>(model));
            return RedirectToAction("Index",new {id=model.StaffId });

        }
    }
}