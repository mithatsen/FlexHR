﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.DTO.Dtos.StaffPersonalInfoDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IMapper _mapper;
        private readonly IStaffRoleService _staffRoleService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly IRoleService _roleService;
        private readonly IStaffCareerService _careerService;
        private readonly IStaffPersonelInfoService _staffPersonelInfoService;
        private readonly IStaffOtherInfoService _staffOtherInfoService;
        private readonly ITownService _townService;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyBranchService _companyBranchService;
        private readonly IStaffCareerService _staffCareerService;
        public StaffController(IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService,
                                     IStaffRoleService staffRoleService, IGeneralSubTypeService generalSubTypeService,
                                     IRoleService roleService, IStaffCareerService careerService, IStaffPersonelInfoService staffPersonelInfoService,
                                     IStaffOtherInfoService staffOtherInfoService, ITownService townService, ICityService cityService, ICountryService countryService,
                                     ICompanyService companyService, ICompanyBranchService companyBranchService, IStaffCareerService staffCareerService
                                )
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _staffRoleService = staffRoleService;
            _generalSubTypeService = generalSubTypeService;
            _roleService = roleService;
            _careerService = careerService;
            _staffPersonelInfoService = staffPersonelInfoService;
            _staffOtherInfoService = staffOtherInfoService;
            _townService = townService;
            _cityService = cityService;
            _countryService = countryService;
            _companyService = companyService;
            _companyBranchService = companyBranchService;
            _staffCareerService = staffCareerService;
        }

        public IActionResult Index()
        {
            var result = _staffService.GetAll();
            ViewBag.ContractTypes = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType), "GeneralSubTypeId", "Description");
            ViewBag.Roles = new SelectList(_roleService.GetAll(), "RoleId", "Name");
            return View(_mapper.Map<List<ListStaffDto>>(result));
        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var result = _staffService.GetAllTables(id);
            var careerResult = _careerService.GetAllTableByStaffId(id);
            var maritialStatusList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.MaritalStatus);
            var degreeOfDisabilityList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.DegreeOfDisability);
            var educationStatusList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationStatus);
            var genderList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Gender);
            var accountTypeList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.AccountType);
            var bloodGroupList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.BloodGroup);
            var educationLevelList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.EducationLevel);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var temp2 = _staffRoleService.GetUserRoleByStaffId(id);
            var personelInfo = _staffPersonelInfoService.GetPersonelInfoByStaffId(id);
            var staffOtherInfo = _staffOtherInfoService.GetOtherInfoByStaffId(id);


            TownHelper cityIdAndCountryId = new TownHelper();

            if (staffOtherInfo.TownId != null)
            {
                cityIdAndCountryId = _townService.GetCityIdAndCountryIdByTownId((int)staffOtherInfo.TownId);

                ViewBag.Cities = new SelectList(_cityService.GetCityListByCountryId(cityIdAndCountryId.CountryId), "CityId", "Name");
                ViewBag.Towns = new SelectList(_townService.GetTownListByCityId(cityIdAndCountryId.CityId), "TownId", "Name");
            }



            var departmentName = "";
            var superscription = "";
            var contractType = "";
            var modeOfOperation = "";
            int contractTypeId = -1;

            ViewBag.Countries = new SelectList(_countryService.GetAll(), "CountryId", "Name");
            ViewBag.Companies = new SelectList(_companyService.GetAll(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department), "GeneralSubTypeId", "Description");
            ViewBag.ModeOfOperations = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation), "GeneralSubTypeId", "Description");
            ViewBag.Titles = new SelectList(_generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title), "GeneralSubTypeId", "Description");


            var staffInfo = _staffGeneralSubTypeService.GetGeneralSubTypeByStaffGeneralSubTypeList(temp);
            for (int i = 0; i < staffInfo.Count; i++)
            {
                int generalTypeId = Convert.ToInt32(staffInfo.GetKey(i));
                if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Department)
                {
                    departmentName = staffInfo[i];
                }
                else if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Title)
                {
                    superscription = staffInfo[i];
                }
                else if (generalTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractType = staffInfo[i];

                }

                else if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.MaritalStatus)
                {
                    modeOfOperation = staffInfo[i];
                }

            }


            foreach (var item in temp)
            {
                if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractTypeId = item.GeneralSubTypeId;
                }
            }


            var careerModels = new List<ListStaffCareerDto>();
            foreach (var item in careerResult)
            {
                var careerModel = new ListStaffCareerDto
                {

                    JobStartDate = item.JobStartDate,
                    JobFinishDate = item.JobFinishDate,
                    CompanyName = item.CompanyBranch.Company.CompanyName,
                    //BranchName = item.CompanyBranch.BranchName,
                    IsActive = item.IsActive,
                    ModeOfOperation = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.ModeOfOperationGeneralSubTypeId),
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.DepartmantGeneralSubTypeId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(item.TitleGeneralSubTypeId)

                };
                careerModels.Add(careerModel);
            }
            var roleList = _roleService.GetAll();
            var contractTypeList = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType);
            var model = new UpdateStaffDto
            {
                StaffId = result.StaffId,
                EmailJob = result.EmailJob,
                EmailPersonal = result.EmailPersonal,
                JobJoinDate = result.JobJoinDate,
                NameSurname = result.NameSurname,
                PhoneJob = result.PhoneJob,
                PhonePersonal = result.PhonePersonal,
                DepartmantName = departmentName,
                Superscription = superscription,
                ContractTypeList = contractTypeList,
                IsActive = result.IsActive,
                ContractType = contractType,
                Roles = roleList,
                RoleId = temp2.RoleId,
                JobFinishDate = result.JobFinishDate,
                BloodGroupList = bloodGroupList,
                GenderList = genderList,
                EducationLevelList = educationLevelList,
                MaritialStatusList = maritialStatusList,
                EducationStatusList = educationStatusList,
                DegreeOfDisabilityList = degreeOfDisabilityList,
                ListStaffCareer = careerModels,
                StaffPersonelInfo = personelInfo,
                StaffOtherInfo = staffOtherInfo,
                AccountTypeList = accountTypeList,
                TownHelper = cityIdAndCountryId,
                ContractTypeId = contractTypeId,
                TownId = staffOtherInfo.TownId == null ? 0 : (int)staffOtherInfo.TownId

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStaffGeneral(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                var temp = _staffGeneralSubTypeService.GetByStaffId(model.StaffId);
                var temp2 = _staffRoleService.GetUserRoleByStaffId(model.StaffId);
                var counter = 0;
                foreach (var item in temp)
                {
                    if (item.GeneralSubType.GeneralTypeId == (int)GeneralTypeEnum.ContractType)
                    {
                        counter++;
                        item.GeneralSubTypeId = model.ContractTypeId;
                    }
                }
                if (counter == 0 && model.ContractTypeId != 0)
                {
                    _staffGeneralSubTypeService.Add(new StaffGeneralSubType
                    {
                        GeneralSubTypeId = model.ContractTypeId,
                        StaffId = model.StaffId
                    });
                }
                temp2.RoleId = model.RoleId;
                _staffRoleService.Update(temp2);
                model.Password = "abc";
                model.UserName = "abc";
                _staffService.Update(_mapper.Map<Staff>(model));
                return RedirectToAction("UpdateStaff", new { id = model.StaffId });
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateStaffPersonalInfo(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                model.StaffPersonelInfo.StaffId = model.StaffId;
                _staffPersonelInfoService.Update(model.StaffPersonelInfo);
                return RedirectToAction("UpdateStaff", new { id = model.StaffId });
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateStaffOtherInfo(UpdateStaffDto model)
        {
            if (ModelState.IsValid)
            {
                model.StaffOtherInfo.StaffId = model.StaffId;
                _staffOtherInfoService.Update(model.StaffOtherInfo);
                return RedirectToAction("UpdateStaff", new { id = model.StaffId });
            }
            return View();
        }
        public JsonResult TownList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_townService.GetTownListByCityId(id));
            return Json(jsonString);
        }
        public JsonResult CityList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_cityService.GetCityListByCountryId(id));
            return Json(jsonString);
        }
        public JsonResult BranchList(int id)
        {
            var jsonString = JsonConvert.SerializeObject(_companyBranchService.GetCompanyBranchListByCompanyId(id));
            return Json(jsonString);
        }

        [HttpPost]
        public IActionResult AddStaffWithAjax(AddStaffDto modal)
        {
            var staffResult = new Staff();
            if (modal.WillUseSystem == false)
            {
                staffResult = _staffService.AddResult(new Staff
                {
                    NameSurname = modal.NameSurname,
                    JobJoinDate = modal.JobJoinDate,
                    IsActive = true,
                    WillUseSystem = modal.WillUseSystem,
                    EmailJob = modal.EmailJob,
                    EmailPersonal = modal.EmailPersonal,
                    JobFinishDate = modal.JobFinishDate,
                    PhoneJob = modal.PhoneJob,
                    PhonePersonal = modal.PhonePersonal,
                });
            }
            else
            {
                staffResult = _staffService.AddResult(new Staff
                {
                    NameSurname = modal.NameSurname,
                    JobJoinDate = modal.JobJoinDate,
                    IsActive = true,
                    UserName = modal.UserName,
                    Password = modal.Password,
                    WillUseSystem = modal.WillUseSystem,
                    EmailJob = modal.EmailJob,
                    EmailPersonal = modal.EmailPersonal,
                    JobFinishDate = modal.JobFinishDate,
                    PhoneJob = modal.PhoneJob,
                    PhonePersonal = modal.PhonePersonal,
                });
            }

            var staffId = staffResult.StaffId;
            if (modal.ContractTypeId != -1)
            {
                _staffGeneralSubTypeService.Add(new StaffGeneralSubType { StaffId = staffId, GeneralSubTypeId = modal.ContractTypeId });
            }
            _staffRoleService.Add(new StaffRole { StaffId = staffId, RoleId = modal.RoleId });
            _staffOtherInfoService.Add(new StaffOtherInfo { StaffId = staffId, IsActive = true });
            _staffPersonelInfoService.Add(new StaffPersonelInfo { StaffId = staffId, IsActive = true });

            return Json("");
        }

        [HttpPost]
        public IActionResult AddStaffCareerWithAjax(AddStaffCareerDto model)
        {
            if (model.CompanyBranchId == -1) 
            {
                model.CompanyBranchId = null;
            }
            _staffCareerService.Add(_mapper.Map<StaffCareer>(model));


            return RedirectToAction("UpdateStaff", new { id = model.StaffId });
        }
    }
}
